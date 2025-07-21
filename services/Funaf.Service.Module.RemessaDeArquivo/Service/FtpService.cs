using System;
using System.Net;
using System.IO;

namespace RemessaBB.FUNAF.Service
{
    public class FtpService
    {
        public string Url { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FileName { get; set; }
        public string FolderName { get; set; }
        public FtpWebRequest ftpWebRequest { get; set; }

        public FtpService(string url, string login, string senha)
        {
            Login = login;
            Url = url;
            Password = senha;
        }

        public void Config(FtpWebRequest request, string folderName, string fileName)
        {
            try
            {
                if (Url == string.Empty)
                    throw new ArgumentException("A URL da conexão é obrigatória", "Url");

                if (Login == string.Empty)
                    throw new ArgumentException("O login de acesso é obrigatório", "Login");

                if (Password == string.Empty)
                    throw new ArgumentException("A senha de acesso é obrigatória", "Password");

                if (request != null)
                {
                    ftpWebRequest = request;
                }
                else
                {
                    if (folderName != null && fileName != null)
                    {
                        FolderName = folderName;
                        FileName = fileName;
                        string absoluteFileName = Path.GetFileName(FileName);

                        request = WebRequest.Create(new Uri(string.Format(@"ftp://{0}/{1}/{2}:22", Url, FolderName, absoluteFileName))) as FtpWebRequest;
                        request.UseBinary = true;
                        request.UsePassive = true;
                        request.KeepAlive = true;
                        request.EnableSsl = true;
                        request.ConnectionGroupName = "group";
                        request.Credentials = new NetworkCredential(Login, Password);

                        ftpWebRequest = request;
                    }
                }
            }
            catch(Exception ex)
            {
                var t = ex;
            }
        }

        public bool Upload()
        {
            try
            {
                FtpWebRequest request = ftpWebRequest;
                request.Method = WebRequestMethods.Ftp.UploadFile;

                using (FileStream fs = File.OpenRead(FileName))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    fs.Close();
                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(buffer, 0, buffer.Length);
                    requestStream.Flush();
                    requestStream.Close();

                    return true;
                }
            }
            catch(Exception ex)
            {
                var t = ex;
                return false;
            }
        }

        public bool Download()
        {
            try
            {
                FtpWebRequest request = ftpWebRequest;
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            using (StreamWriter destination = new StreamWriter(FileName))
                            {
                                destination.Write(reader.ReadToEnd());
                                destination.Flush();

                                return true;
                            }
                        }
                    }
                }
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}