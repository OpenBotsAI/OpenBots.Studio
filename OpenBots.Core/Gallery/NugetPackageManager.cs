﻿using Autofac;
using Newtonsoft.Json;
using NuGet.Common;
using NuGet.Configuration;
using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Packaging.Signing;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Resolver;
using NuGet.Versioning;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OpenBots.Core.Gallery
{
    public class NugetPackageManager
    {
        public static async Task<List<NuGetVersion>> GetPackageVersions(string packageId, string source, bool includePrerelease)
        {
            ILogger logger = NullLogger.Instance;
            CancellationToken cancellationToken = CancellationToken.None;

            SourceCacheContext cache = new SourceCacheContext();
            SourceRepository repository = Repository.Factory.GetCoreV3(source);
            FindPackageByIdResource resource = await repository.GetResourceAsync<FindPackageByIdResource>();

            IEnumerable<NuGetVersion> versions = await resource.GetAllVersionsAsync(
                packageId,
                cache,
                logger,
                cancellationToken);

            if (includePrerelease)
                return versions.ToList();
            else
                return versions.Where(x => x.IsPrerelease == false).ToList();
        }

        public static async Task<List<IPackageSearchMetadata>> SearchPackages(string packageKeyword, string source, bool includePrerelease)
        {
            ILogger logger = NullLogger.Instance;
            CancellationToken cancellationToken = CancellationToken.None;

            SourceRepository repository = Repository.Factory.GetCoreV3(source);
            PackageSearchResource resource = await repository.GetResourceAsync<PackageSearchResource>();
            SearchFilter searchFilter = new SearchFilter(includePrerelease: includePrerelease);

            IEnumerable<IPackageSearchMetadata> results = await resource.SearchAsync(
                packageKeyword,
                searchFilter,
                skip: 0,
                take: 20,
                logger,
                cancellationToken);

            return results.ToList();
        }

        public static async Task<List<IPackageSearchMetadata>> GetPackageMetadata(string packageId, string source, bool includePrerelease)
        {
            ILogger logger = NullLogger.Instance;
            CancellationToken cancellationToken = CancellationToken.None;

            SourceCacheContext cache = new SourceCacheContext();
            SourceRepository repository = Repository.Factory.GetCoreV3(source);
            PackageMetadataResource resource = await repository.GetResourceAsync<PackageMetadataResource>();

            IEnumerable<IPackageSearchMetadata> packages = await resource.GetMetadataAsync(
                packageId,
                includePrerelease: includePrerelease,
                includeUnlisted: true,
                cache,
                logger,
                cancellationToken);

            return packages.ToList();
        }

        public static async Task DownloadPackage(string packageId, string version, string packageLocation, string packageName, string feed)
        {
            ILogger logger = NullLogger.Instance;
            CancellationToken cancellationToken = CancellationToken.None;

            SourceCacheContext cache = new SourceCacheContext();
            SourceRepository repository = Repository.Factory.GetCoreV3(feed);
            FindPackageByIdResource resource = await repository.GetResourceAsync<FindPackageByIdResource>();

            NuGetVersion packageVersion = new NuGetVersion(version);
            using (MemoryStream packageStream = new MemoryStream()) {

                bool success = await resource.CopyNupkgToStreamAsync(
                packageId,
                packageVersion,
                packageStream,
                cache,
                logger,
                cancellationToken);

                var path = Path.Combine(packageLocation, packageName + ".nupkg");
                using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    packageStream.WriteTo(fileStream);
                }
            }
        }

        public static async Task InstallPackage(string packageId, string version, Dictionary<string, string> projectDependenciesDict)
        {
            var packageVersion = NuGetVersion.Parse(version);
            var nuGetFramework = NuGetFramework.ParseFolder("net48");
            var settings = NuGet.Configuration.Settings.LoadDefaultSettings(root: null);
            var sourceRepositoryProvider = new SourceRepositoryProvider(settings, Repository.Provider.GetCoreV3());

            using (var cacheContext = new SourceCacheContext())
            {
                var galleryRepo = sourceRepositoryProvider.CreateRepository(new PackageSource("https://dev.gallery.openbots.io/v3/index.json", "OpenBots Gallery", true));
                var repositories = sourceRepositoryProvider.GetRepositories().ToList();
                repositories.Add(galleryRepo);

                var availablePackages = new HashSet<SourcePackageDependencyInfo>(PackageIdentityComparer.Default);
                await GetPackageDependencies(
                    new PackageIdentity(packageId, packageVersion),
                    nuGetFramework, cacheContext, NullLogger.Instance, repositories, availablePackages);

                var resolverContext = new PackageResolverContext(
                    DependencyBehavior.Lowest,
                    new[] { packageId },
                    Enumerable.Empty<string>(),
                    Enumerable.Empty<PackageReference>(),
                    Enumerable.Empty<PackageIdentity>(),
                    availablePackages,
                    sourceRepositoryProvider.GetRepositories().Select(s => s.PackageSource),
                    NullLogger.Instance);

                var resolver = new PackageResolver();
                var packagesToInstall = resolver.Resolve(resolverContext, CancellationToken.None)
                    .Select(p => availablePackages.Single(x => PackageIdentityComparer.Default.Equals(x, p)));
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var packagePathResolver = new PackagePathResolver(Path.Combine(appDataPath, "OpenBots Inc", "packages"));
                var packageExtractionContext = new PackageExtractionContext(
                    PackageSaveMode.Defaultv3,
                    XmlDocFileSaveMode.None,
                    ClientPolicyContext.GetClientPolicy(settings, NullLogger.Instance),
                    NullLogger.Instance);

                var frameworkReducer = new FrameworkReducer();

                foreach (var packageToInstall in packagesToInstall)
                {
                    PackageReaderBase packageReader;
                    var installedPath = packagePathResolver.GetInstalledPath(packageToInstall);
                    if (installedPath == null)
                    {
                        var downloadResource = await packageToInstall.Source.GetResourceAsync<DownloadResource>(CancellationToken.None);
                        var downloadResult = await downloadResource.GetDownloadResourceResultAsync(
                            packageToInstall,
                            new PackageDownloadContext(cacheContext),
                            SettingsUtility.GetGlobalPackagesFolder(settings),
                            NullLogger.Instance, CancellationToken.None);

                        await PackageExtractor.ExtractPackageAsync(
                            downloadResult.PackageSource,
                            downloadResult.PackageStream,
                            packagePathResolver,
                            packageExtractionContext,
                            CancellationToken.None);

                        packageReader = downloadResult.PackageReader;
                    }
                    else
                    {
                        packageReader = new PackageFolderReader(installedPath);
                    }

                    var libItems = packageReader.GetLibItems();
                    var nearest = frameworkReducer.GetNearest(nuGetFramework, libItems.Select(x => x.TargetFramework));                            

                    if (packageToInstall.Id == packageId)
                    {
                        string packageListAssemblyPath = libItems
                        .Where(x => x.TargetFramework.Equals(nearest))
                        .SelectMany(x => x.Items.Where(i => i.EndsWith(".dll"))).FirstOrDefault();

                        var existingPackage = projectDependenciesDict.Where(x => x.Key == packageToInstall.Id)
                                                                     .Select(e => (KeyValuePair<string, string>?)e)
                                                                     .FirstOrDefault();
                        if (existingPackage != null)
                            projectDependenciesDict.Remove(((KeyValuePair<string, string>)existingPackage).Key);

                        projectDependenciesDict.Add(packageToInstall.Id, packageToInstall.Version.ToString());
                    }
                }
            }            
        }

        public static async Task GetPackageDependencies(PackageIdentity package,
                NuGetFramework framework,
                SourceCacheContext cacheContext,
                ILogger logger,
                IEnumerable<SourceRepository> repositories,
                ISet<SourcePackageDependencyInfo> availablePackages)
        {
            if (availablePackages.Contains(package)) return;

            foreach (var sourceRepository in repositories)
            {
                var dependencyInfoResource = await sourceRepository.GetResourceAsync<DependencyInfoResource>();
                var dependencyInfo = await dependencyInfoResource.ResolvePackage(
                    package, framework, cacheContext, logger, CancellationToken.None);

                if (dependencyInfo == null) continue;

                availablePackages.Add(dependencyInfo);
                foreach (var dependency in dependencyInfo.Dependencies)
                {
                    await GetPackageDependencies(
                        new PackageIdentity(dependency.Id, dependency.VersionRange.MinVersion),
                        framework, cacheContext, logger, repositories, availablePackages);
                }
            }
        }

        public static async Task<List<string>> LoadProjectAssemblies(string configPath)
        {
            List<string> assemblyPaths = new List<string>();
            var dependencies = JsonConvert.DeserializeObject<Project.Project>(File.ReadAllText(configPath)).Dependencies;

            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string packagePath = Path.Combine(appDataPath, "OpenBots Inc", "packages");

            foreach (var dependency in dependencies)
            {
                var packageId = dependency.Key;
                var packageVersion = NuGetVersion.Parse(dependency.Value);
                var nuGetFramework = NuGetFramework.ParseFolder("net48");
                var settings = NuGet.Configuration.Settings.LoadDefaultSettings(root: null);

                var sourceRepositoryProvider = new SourceRepositoryProvider(settings, Repository.Provider.GetCoreV3());

                using (var cacheContext = new SourceCacheContext())
                {
                    var galleryRepo = sourceRepositoryProvider.CreateRepository(new PackageSource("https://dev.gallery.openbots.io/v3/index.json", "OpenBots Gallery", true));
                    var repositories = sourceRepositoryProvider.GetRepositories().ToList();
                    repositories.Add(galleryRepo);

                    var availablePackages = new HashSet<SourcePackageDependencyInfo>(PackageIdentityComparer.Default);
                    await GetPackageDependencies(
                        new PackageIdentity(packageId, packageVersion),
                        nuGetFramework, cacheContext, NullLogger.Instance, repositories, availablePackages);

                    var resolverContext = new PackageResolverContext(
                        DependencyBehavior.Lowest,
                        new[] { packageId },
                        Enumerable.Empty<string>(),
                        Enumerable.Empty<PackageReference>(),
                        Enumerable.Empty<PackageIdentity>(),
                        availablePackages,
                        sourceRepositoryProvider.GetRepositories().Select(s => s.PackageSource),
                        NullLogger.Instance);

                    var resolver = new PackageResolver();
                    var packagesToInstall = resolver.Resolve(resolverContext, CancellationToken.None)
                        .Select(p => availablePackages.Single(x => PackageIdentityComparer.Default.Equals(x, p)));

                    var packagePathResolver = new PackagePathResolver(packagePath);//Path.GetFullPath("packages"));
                    var packageExtractionContext = new PackageExtractionContext(
                        PackageSaveMode.Defaultv3,
                        XmlDocFileSaveMode.None,
                        ClientPolicyContext.GetClientPolicy(settings, NullLogger.Instance),
                        NullLogger.Instance);

                    var frameworkReducer = new FrameworkReducer();

                    foreach (var packageToInstall in packagesToInstall)
                    {
                        PackageReaderBase packageReader;
                        var installedPath = packagePathResolver.GetInstalledPath(packageToInstall);

                        packageReader = new PackageFolderReader(installedPath);

                        var libItems = packageReader.GetLibItems();
                        var nearest = frameworkReducer.GetNearest(nuGetFramework, libItems.Select(x => x.TargetFramework));

                        string packageListAssemblyPath = libItems
                            .Where(x => x.TargetFramework.Equals(nearest))
                            .SelectMany(x => x.Items.Where(i => i.EndsWith(".dll"))).FirstOrDefault();

                        var dependencyPath = Path.Combine(packagePath, $"{packageToInstall.Id}.{packageToInstall.Version}", packageListAssemblyPath);

                        if (!assemblyPaths.Contains(dependencyPath))
                            assemblyPaths.Add(dependencyPath);
                    }
                }
            }

            return assemblyPaths;
        }        
    }
}