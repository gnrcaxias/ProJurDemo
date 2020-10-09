using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using ProJur.Business.Dto;
using InfoVillage.DevLibrary;
using System.Net;
using System.IO;

namespace ProJur.Business.Bll
{

    [DataObject(true)]
    public class bllBackup
    {

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoBackup> GetAll()
        {
            return GetAll("");
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoBackup> GetAll(string SortExpression)
        {
            List<dtoBackup> backups = new List<dtoBackup>();
            dtoBackupConfiguracaoFTP configuracaoFTP = bllBackupConfiguracaoFTP.Get(1);

            if (configuracaoFTP != null)
            {
                if (configuracaoFTP.Host.Trim() != String.Empty
                    && configuracaoFTP.Usuario.Trim() != String.Empty
                    && configuracaoFTP.Senha.Trim() != String.Empty)
                {

                    if (configuracaoFTP.Host.IndexOf("ftp:") < 0)
                        configuracaoFTP.Host = String.Format("{0}{1}", @"ftp://", configuracaoFTP.Host);
                    
                    if (configuracaoFTP.Host.Trim().LastIndexOf("/") != configuracaoFTP.Host.Trim().Length - 1)
                        configuracaoFTP.Host = String.Format("{0}{1}", configuracaoFTP.Host, @"/");

                    //FTP Server URL.
                    string ftp = configuracaoFTP.Host;

                    //FTP Folder name. Leave blank if you want to list files from root folder.
                    string ftpFolder = "";

                    try
                    {
                        //Create FTP Request.
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + ftpFolder);
                        request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                        //Enter FTP Server credentials.
                        request.Credentials = new NetworkCredential(configuracaoFTP.Usuario, configuracaoFTP.Senha);
                        request.UsePassive = configuracaoFTP.Passivo;
                        request.UseBinary = true;
                        request.EnableSsl = false;

                        //Fetch the Response and read it using StreamReader.
                        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                        List<string> entries = new List<string>();
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            //Read the Response as String and split using New Line character.
                            entries = reader.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        }
                        response.Close();

                        //Loop and add details of each File to the DataTable.
                        foreach (string entry in entries)
                        {
                            string[] splits = entry.Split(new string[] { " ", }, StringSplitOptions.RemoveEmptyEntries);

                            //Determine whether entry is for File or Directory.
                            bool isFile = (splits[0].Substring(0, 1) != "d" && splits[3].IndexOf("ftpaccess") < 0);
                            bool isDirectory = splits[0].Substring(0, 1) == "d";

                            //If entry is for File, add details to DataTable.
                            if (isFile)
                            {
                                string[] splitData = splits[0].Split(new string[] { "-", }, StringSplitOptions.RemoveEmptyEntries);

                                dtoBackup backup = new dtoBackup();

                                backup.tamanhoArquivo = ((decimal.Parse(splits[2]) / 1024) / 1024);
                                backup.nomeArquivo = splits[3];
                                backup.dataArquivo = Convert.ToDateTime(string.Join(" ", string.Join("/", splitData[1], splitData[0], splitData[2]), splits[1]));
                                backup.caminhoHttp = configuracaoFTP.caminhoHttp;

                                backups.Add(backup);
                            }
                        }

                        if (backups.Count > 0)
                        {
                            switch (SortExpression.ToUpper())
                            {
                                case "DATAARQUIVO":
                                    backups = backups.OrderBy(o => o.dataArquivo).ToList();

                                    break;

                                case "DATAARQUIVO DESC":
                                    backups = backups.OrderByDescending(o => o.dataArquivo).ToList();


                                    break;

                                case "NOMEARQUIVO":
                                    backups = backups.OrderBy(o => o.nomeArquivo).ToList();

                                    break;

                                case "NOMEARQUIVO DESC":
                                    backups = backups.OrderByDescending(o => o.nomeArquivo).ToList();

                                    break;

                                case "TAMANHOARQUIVO":
                                    backups = backups.OrderBy(o => o.tamanhoArquivo).ToList();

                                    break;

                                case "TAMANHOARQUIVO DESC":
                                    backups = backups.OrderByDescending(o => o.tamanhoArquivo).ToList();

                                    break;

                                default:
                                    break;
                            }


                        }

                        //Bind the GridView.
                        return backups;
                    }
                    catch (WebException ex)
                    {
                        throw new Exception((ex.Response as FtpWebResponse).StatusDescription);
                    }
                }
                else
                    return null;
            }
            else
                return null;
        }

        public static string DeleteFile(string nomeArquivo)
        {
            dtoBackupConfiguracaoFTP configuracaoFTP = bllBackupConfiguracaoFTP.Get(1);
            string retorno = String.Empty;

            if (configuracaoFTP != null)
            {
                if (configuracaoFTP.Host.Trim() != String.Empty
                    && configuracaoFTP.Usuario.Trim() != String.Empty
                    && configuracaoFTP.Senha.Trim() != String.Empty)
                {

                    if (configuracaoFTP.Host.IndexOf("ftp:") < 0)
                        configuracaoFTP.Host = String.Format("{0}{1}", @"ftp://", configuracaoFTP.Host);

                    if (configuracaoFTP.Host.Trim().LastIndexOf("/") != configuracaoFTP.Host.Trim().Length - 1)
                        configuracaoFTP.Host = String.Format("{0}{1}", configuracaoFTP.Host, @"/");

                    //FTP Server URL.
                    string ftp = configuracaoFTP.Host;

                    //FTP Folder name. Leave blank if you want to list files from root folder.
                    string ftpFolder = "";

                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + ftpFolder + nomeArquivo);
                    request.Method = WebRequestMethods.Ftp.DeleteFile;
                    request.Credentials = new NetworkCredential(configuracaoFTP.Usuario, configuracaoFTP.Senha);

                    using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                    {
                        retorno = response.StatusDescription;
                    }
                }
            }

            return retorno;
        }

    }
}
