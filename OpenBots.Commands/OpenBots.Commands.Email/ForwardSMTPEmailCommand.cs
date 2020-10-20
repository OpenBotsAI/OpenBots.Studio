﻿using MailKit.Net.Smtp;
using MimeKit;
using System.ComponentModel;
using OpenBots.Core.Attributes.PropertyAttributes;
using OpenBots.Core.Command;
using OpenBots.Core.Enums;
using OpenBots.Core.Infrastructure;
using OpenBots.Core.Utilities.CommonUtilities;
using OpenBots.Engine;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading;
using System.Windows.Forms;

namespace OpenBots.Commands.Email
{
    [Serializable]
    [Category("Email Commands")]
    [Description("This command forwards a selected email using SMTP protocol.")]

    public class ForwardSMTPEmailCommand : ScriptCommand
    {

        [DisplayName("MimeMessage")]
        [Description("Enter the MimeMessage to forward.")]
        [SampleUsage("{vMimeMessage}")]
        [Remarks("")]
        [PropertyUIHelper(UIAdditionalHelperType.ShowVariableHelper)]
        public string v_SMTPMimeMessage { get; set; }

        [DisplayName("Host")]
        [Description("Define the host/service name that the script should use.")]
        [SampleUsage("smtp.gmail.com || {vHost}")]
        [Remarks("")]
        [PropertyUIHelper(UIAdditionalHelperType.ShowVariableHelper)]
        public string v_SMTPHost { get; set; }

        [DisplayName("Port")]
        [Description("Define the port number that should be used when contacting the SMTP service.")]
        [SampleUsage("465 || {vPort}")]
        [Remarks("")]
        [PropertyUIHelper(UIAdditionalHelperType.ShowVariableHelper)]
        public string v_SMTPPort { get; set; }

        [DisplayName("Username")]
        [Description("Define the username to use when contacting the SMTP service.")]
        [SampleUsage("myRobot || {vUsername}")]
        [Remarks("")]
        [PropertyUIHelper(UIAdditionalHelperType.ShowVariableHelper)]
        public string v_SMTPUserName { get; set; }

        [DisplayName("Password")]
        [Description("Define the password to use when contacting the SMTP service.")]
        [SampleUsage("password || {vPassword}")]
        [Remarks("")]
        [PropertyUIHelper(UIAdditionalHelperType.ShowVariableHelper)]
        public string v_SMTPPassword { get; set; }

        [DisplayName("Recipient(s)")]
        [Description("Enter the email address(es) of the recipient(s).")]
        [SampleUsage("test@test.com || test@test.com;test2@test.com || {vEmail} || {vEmail1};{vEmail2} || {vEmails}")]
        [Remarks("Multiple recipient email addresses should be delimited by a semicolon (;).")]
        [PropertyUIHelper(UIAdditionalHelperType.ShowVariableHelper)]
        public string v_SMTPRecipients { get; set; }

        [DisplayName("Email Body")]
        [Description("Enter text to be used as the email body.")]
        [SampleUsage("Everything ran ok at {DateTime.Now}  || {vBody}")]
        [Remarks("")]
        [PropertyUIHelper(UIAdditionalHelperType.ShowVariableHelper)]
        public string v_SMTPBody { get; set; }

        public ForwardSMTPEmailCommand()
        {
            CommandName = "ForwardSMTPEmailCommand";
            SelectionName = "Forward SMTP Email";
            CommandEnabled = true;
            CustomRendering = true;
        }

        public override void RunCommand(object sender)
        {
            var engine = (AutomationEngineInstance)sender;
            MimeMessage vMimeMessageToForward = (MimeMessage)v_SMTPMimeMessage.ConvertUserVariableToObject(engine);
            string vSMTPHost = v_SMTPHost.ConvertUserVariableToString(engine);
            string vSMTPPort = v_SMTPPort.ConvertUserVariableToString(engine);
            string vSMTPUserName = v_SMTPUserName.ConvertUserVariableToString(engine);
            string vSMTPPassword = v_SMTPPassword.ConvertUserVariableToString(engine);
            string vSMTPRecipients = v_SMTPRecipients.ConvertUserVariableToString(engine);
            string vSMTPBody = v_SMTPBody.ConvertUserVariableToString(engine);

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (sndr, certificate, chain, sslPolicyErrors) => true;
                client.SslProtocols = SslProtocols.None;

                using (var cancel = new CancellationTokenSource())
                {
                    try
                    {
                        client.Connect(vSMTPHost, int.Parse(vSMTPPort), true, cancel.Token); //SSL
                    }
                    catch (Exception)
                    {
                        client.Connect(vSMTPHost, int.Parse(vSMTPPort)); //TLS
                    }

                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(vSMTPUserName, vSMTPPassword, cancel.Token);

                    //construct a new message
                    var message = new MimeMessage();
                    message.From.Add(MailboxAddress.Parse(vSMTPUserName));
                    message.ReplyTo.Add(MailboxAddress.Parse(vSMTPUserName));

                    var splitRecipients = vSMTPRecipients.Split(';');
                    foreach (var vSMTPToEmail in splitRecipients)
                        message.To.Add(MailboxAddress.Parse(vSMTPToEmail));

                    message.Subject = "Fwd: " + vMimeMessageToForward.Subject;

                    //create a body
                    var builder = new BodyBuilder();
                    builder.TextBody = vSMTPBody;
                    builder.Attachments.Add(new MessagePart { Message = vMimeMessageToForward });
                    message.Body = builder.ToMessageBody();

                    client.Send(message);
                    client.ServerCertificateValidationCallback = null;
                }
            }                    
        }

        public override List<Control> Render(IfrmCommandEditor editor, ICommandControls commandControls)
        {
            base.Render(editor, commandControls);

            RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_SMTPMimeMessage", this, editor));
            RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_SMTPHost", this, editor));
            RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_SMTPPort", this, editor));
            RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_SMTPUserName", this, editor));
            RenderedControls.AddRange(commandControls.CreateDefaultPasswordInputGroupFor("v_SMTPPassword", this, editor));
            RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_SMTPRecipients", this, editor));
            RenderedControls.AddRange(commandControls.CreateDefaultInputGroupFor("v_SMTPBody", this, editor, 100, 300));

            return RenderedControls;
        }

        public override string GetDisplayValue()
        {
            return base.GetDisplayValue() + $" [MimeMessage '{v_SMTPMimeMessage}' - Forward to '{v_SMTPRecipients}']";
        }
    }
}
