using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text;

namespace Phalanx.Common
{
    public class MyMail
    {
        public string Titulo { get; set;  }
         public string De {get;set;}
         public string Para {get;set;} 
         public string Corpo {get;set;}
         public string Processo {get;set;}
         public bool CorpoHTML { get; set; }

        public string SMTP {get;set;}
         public string Senha {get;set;} 
         public string Usuario {get;set;}

         public MyMail()
         {

             SMTP       =  db.GetParam("SERVMAIL").ToString();
             Senha      =  db.GetParam("SENHAMAIL").ToString();
             Usuario    =  db.GetParam("LOGMAIL").ToString();  
         }
         

         public bool Enviar()
         {

             //Define os dados do e-mail
             //string nomeRemetente = "";
             string emailRemetente = this.De;

             string emailDestinatario = this.Para;
             //string emailComCopia = "";
             //string emailComCopiaOculta = "";

             string assuntoMensagem = this.Titulo;
             string conteudoMensagem = this.Corpo;

             //Cria objeto com dados do e-mail.
             MailMessage objEmail = new MailMessage();

             //Define o Campo From e ReplyTo do e-mail.
             objEmail.From = new System.Net.Mail.MailAddress(emailRemetente);

             //Define os destinatários do e-mail.
             objEmail.To.Add(emailDestinatario);

             //Enviar cópia para.
             //objEmail.CC.Add(emailComCopia);

             //Enviar cópia oculta para.
             //objEmail.Bcc.Add(emailComCopiaOculta);

             //Define a prioridade do e-mail.
             objEmail.Priority = System.Net.Mail.MailPriority.Normal;

             //Define o formato do e-mail HTML (caso não queira HTML alocar valor false)
             objEmail.IsBodyHtml = this.CorpoHTML;

             //Define título do e-mail.
             objEmail.Subject = assuntoMensagem;

             //Define o corpo do e-mail.
             objEmail.Body = conteudoMensagem;

             //Para evitar problemas de caracteres "estranhos",  charset para "ISO-8859-1"
             objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
             objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");


             
             //Caminho do arquivo a ser enviado como anexo
             //string arquivo = Server.MapPath("arquivo.jpg");

             // Ou especifique o caminho manualmente
             //string arquivo = @"e:\home\LoginFTP\Web\arquivo.jpg";

             // Cria o anexo para o e-mail
             //Attachment anexo = new Attachment(arquivo, System.Net.Mime.MediaTypeNames.Application.Octet);

             // Anexa o arquivo a mensagemn
             //objEmail.Attachments.Add(anexo);

             //Cria objeto com os dados do SMTP
             System.Net.Mail.SmtpClient objSmtp = new System.Net.Mail.SmtpClient();

             //Alocamos o endereço do host para enviar os e-mails, localhost(recomendado) 
            // objSmtp.Host = "localhost";
             

             objSmtp.Host = SMTP;
             //objSmtp.Port = 587;
             objSmtp.Credentials = new NetworkCredential(Usuario, Senha);

             bool ret = true;
             //Enviamos o e-mail através do método .send()
             try
             {
                 objSmtp.Send(objEmail);
                 util.mens("E-mail sent successfully !");
             }
             catch (Exception ex)
             {
                 ret = false;
                 string err = "Problems sending e-mail. Error = " + ex.Message;
                 string filename = DateTime.Now.ToString().Replace("/","").Replace(":","").Replace(" ","_").Substring(0,13)+"_mail_error.txt";
                 util.mens(err);
                 StreamWriter log = new StreamWriter(filename, true, Encoding.ASCII);
                 log.WriteLine(err + " | " + ex.InnerException);
                 log.Close();
             }
             finally
             {
                 
                 objEmail.Dispose();
                 //anexo.Dispose();
             }
 

             return ret;
         }

    }
}
