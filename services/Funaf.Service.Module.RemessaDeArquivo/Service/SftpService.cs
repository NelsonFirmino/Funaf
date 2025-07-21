using Renci.SshNet;
using System;
using System.IO;

namespace RemessaBB.FUNAF.Service
{
    // https://github.com/sshnet/SSH.NET/blob/develop/src/Renci.SshNet.Tests/Classes/SftpClientTest.Upload.cs
    public class SftpService
    {
        public string Url { get; set; }
        public int Port { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FileName { get; set; }
        public string LocalFolderName { get; set; }
        public string RemoteFolderName { get; set; }

        public SftpService(string url, int port, string login, string senha)
        {
            Url = url;
            Port = Port;
            Login = login;
            Password = senha;
        }

        public void Config(string localFolderName, string remoteFolderName, string fileName)
        {
            try
            {
                if (Url.Length < 0)
                    throw new ArgumentException("A URL da conexão é obrigatória", "Url");

                if (Login.Length < 0)
                    throw new ArgumentException("O login de acesso é obrigatório", "Login");

                if (Password.Length < 0)
                    throw new ArgumentException("A senha de acesso é obrigatória", "Password");

                if (localFolderName.Length < 0)
                    throw new ArgumentException("A senha de acesso é obrigatória", "Password");

                if (remoteFolderName.Length < 0)
                    throw new ArgumentException("A senha de acesso é obrigatória", "Password");

                if (fileName.Length < 0)
                    throw new ArgumentException("A senha de acesso é obrigatória", "Password");

                LocalFolderName = localFolderName;
                RemoteFolderName = remoteFolderName;
                FileName = fileName;
            }
            catch (Exception ex)
            {
                var t = ex;
            }
        }

        public bool Upload()
        {
            try
            {
                using (var sftp =  new SftpClient(Url, Port, Login, Password))
                {
                    sftp.Connect();

                    using (var file = File.OpenRead(Path.Combine(LocalFolderName, FileName)))
                    {
                        sftp.UploadFile(file, Path.Combine(RemoteFolderName, FileName));
                    }

                    sftp.Disconnect();

                    return true;
                }
            }
            catch (Exception ex)
            {
                var t = ex;
                return false;
            }
        }

        public bool Download()
        {
            try
            {
                using (var sftp = new SftpClient(Url, Port, Login, Password))
                {
                    sftp.Connect();

                    using (var file = File.OpenWrite(Path.Combine(LocalFolderName, FileName)))
                    {
                        sftp.DownloadFile(Path.Combine(RemoteFolderName, FileName), file);
                    }

                    sftp.Disconnect();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}