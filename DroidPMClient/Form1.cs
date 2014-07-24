using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure;
using System.Net.Sockets;
using System.Globalization;


namespace DroidPMClient
{
    public partial class Form1 : Form
    {
        Timer timer;
        int count = 0;
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool LockWorkStation();

        DataClassesDataContext data = new DataClassesDataContext();
        public Form1()
        {
            InitializeComponent();
            cmdLogout.Enabled = false;
        }


        //now use this class
        //MAC_ADDRESS should  look like '013FA049'
        private void WakeFunction(string MAC_ADDRESS)
        {
            WOLClass client = new WOLClass();

            //255.255.255.255  i.e broadcast 
            client.Connect(new IPAddress(0xffffffff), 0x2fff); // port=12287 let's use this one 

            client.SetClientToBrodcastMode();
            //set sending bites
            int counter = 0;
            //buffer to be send
            byte[] bytes = new byte[1024];   // more than enough :-)
            //first 6 bytes should be 0xFF
            for (int y = 0; y < 6; y++)
                bytes[counter++] = 0xFF;
            //now repeate MAC 16 times
            for (int y = 0; y < 16; y++)
            {
                int i = 0;
                for (int z = 0; z < 6; z++)
                {
                    bytes[counter++] =
                        byte.Parse(MAC_ADDRESS.Substring(i, 2),
                        NumberStyles.HexNumber);
                    i += 2;
                }
            }

            //now send wake up packet
            int reterned_value = client.Send(bytes, 1024);
        }

        public void listBlob()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client. 
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("photos");

            // Loop over items within the container and output the length and URI.
            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;

                    Console.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);

                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob pageBlob = (CloudPageBlob)item;

                    Console.WriteLine("Page blob of length {0}: {1}", pageBlob.Properties.Length, pageBlob.Uri);

                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory directory = (CloudBlobDirectory)item;

                    Console.WriteLine("Directory: {0}", directory.Uri);
                }
            }
        }

        public void downloadBlob()
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("processes");

            // Retrieve reference to a blob named "photo1.jpg".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("myblob");

            // Save blob contents to a file.
            using (var fileStream = System.IO.File.OpenWrite(@"C:/Users/George/Desktop/" + txtUname.Text + "processlistvalues.txt"))
            {
                blockBlob.DownloadToStream(fileStream);
            }
        }

        public void uploadBlob()
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("processes");
            container.CreateIfNotExists();

            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("myblob");

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var fileStream = System.IO.File.OpenRead(@"C:/Users/George/Desktop/" + txtUname.Text + "processlist.txt"))
            {
                blockBlob.UploadFromStream(fileStream);
            }


        }

        public void initTimer()
        {
            timer = new Timer();
            timer.Tick += new EventHandler(timer1_Tick); // Everytime timer ticks, timer_Tick will be called
            timer.Interval = (1000) * (5);              // Timer will tick every second
            timer.Enabled = true;                       // Enable the timer
            timer.Start();                              // Start the timer
        }

        public bool resetCommand(string command)
        {
            if (!command.Equals("IGNORE"))
            {
                CommandItem t = new CommandItem();
                t.command = "IGNORE";
                return updateCommand(txtUname.Text, t);
            }

            return false;
        }

        public bool resetProcess(string command)
        {
            if (!command.Equals("IGNORE"))
            {
                CommandItem t = new CommandItem();
                t.terminateprocess = "IGNORE";
                return updateTerminate(txtUname.Text, t);
            }

            return false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string command = getCommand(txtUname.Text);
            string process = getProcess(txtUname.Text);
            lblcommand.Text = "In Timer Tick : " + count++ + " : " + command;
            ExecuteCommand(command);
            ExecuteCommand(process);


        }

        public void updateProcesses()
        {
            GenerateProcessList();
            uploadBlob();
        }

        public void ExecuteCommand(String command)
        {

            lblprog.Text = command;
            if (command.Equals("HIBERNATE"))
            {
                resetCommand(command);
                Application.SetSuspendState(PowerState.Hibernate, true, true);
                Process.Start("shutdown", "/h /t 3 /f");

            }
            else if (command.Equals("SLEEP"))
            {
                resetCommand(command);
                Application.SetSuspendState(PowerState.Suspend, true, true);
                Process.Start("shutdown", "/s /t 3 /f");

            }
            else if (command.Equals("SHUTDOWN"))
            {
                resetCommand(command);
                MessageBox.Show("Shutdown", "Shutting down computer now");
                Process.Start("shutdown", "/s /t 3 /f");

            }
            else if (command.Equals("RESTART"))
            {
                resetCommand(command);
                MessageBox.Show("Restart", "Restarting computer now");
                Process.Start("shutdown", "/r /t 3 /f");

            }
            else if (command.Equals("LOGOFF"))
            {
                resetCommand(command);
                MessageBox.Show("Shutdown", "Logging off computer now");
                Process.Start("shutdown", "/l /t 3 /f");

            }
            else if (command.Equals("LOCK"))
            {

                resetCommand(command);
                updateProcesses();
                lblprog.Text = "IN LOCK : " + resetCommand(command);
                bool result = LockWorkStation();


            }
            else if (command.Equals("DEFAULT"))
            {

                lblprog.Text = "IN DEFAULT : " + resetCommand(command);

            }
            else if (command.Equals("IGNORE"))
            {
                updateProcesses();

                lblprog.Text = "IN IGNORE : " + count++;

            }
            else if (command.Contains("TERMINATE"))
            {
                string exPath = command.Remove(0, 10);
                TerminateProc(exPath.Substring(0, exPath.Count() - 1).Remove(0, 2));
                updateProcesses();
                resetProcess(command);
            }


            if (command.Contains("RUN"))
            {
                if (resetCommand(command))
                {
                    string exPath = command.Remove(0, 3);
                    Process.Start("cmd", "/C " + exPath);

                    // MessageBox.Show("Run", "Run command received, executing now");
                }


            }
            else if (command.Contains("APPLICATION"))
            {
                resetCommand(command);
                string exPath = command.Remove(0, 11);
                Process.Start("cmd", "/c start " + exPath);
                MessageBox.Show("Application", "Application command received, executing now");

            }
            else if (command.Contains("CUSTOM"))
            {
                resetCommand(command);
                MessageBox.Show("Custom", "Custom command received but cannot handle it");

            }

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Show();
            this.Visible = true;
            this.BringToFront();
            this.Activate();
        }

        private void cmdlogin_Click(object sender, EventArgs e)
        {
            CommandItem t = new CommandItem();

            if (validateUser(txtUname.Text, txtPass.Text))
            {
                MessageBox.Show("Success");
                txtUname.Text = txtUname.Text;
                txtUname.Enabled = false;
                txtPass.Text = "";
                txtPass.Enabled = false;
                cmdlogin.Enabled = false;
                cmdLogout.Enabled = true;

                t.computername = System.Environment.MachineName;
                PostComputerName(txtUname.Text, t);

                initTimer();
            }
            else
            {
                MessageBox.Show("Incorrect details");
            }

        }

        public bool addUser(CommandItem t)
        {
            data.CommandItems.InsertOnSubmit(t);
            try
            {
                data.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;

            }
        }

        public bool validateUser(string UserName, string Password)
        {
            try
            {
                CommandItem j = getUser(UserName);
                string pass = GenerateHash(Password);

                if (j.password != pass)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (System.Exception e)
            {

                return false;

            }




        }

        public string GenerateHash(string pass)
        {
            SHA1 HashAlgorithm = SHA1.Create();
            Byte[] c;
            c = HashAlgorithm.ComputeHash(System.Text.Encoding.Default.GetBytes(pass));
            string hashedpass = "";
            int i;
            for (i = 0; i < c.Length - 1; i++)
            {
                hashedpass += c[i].ToString("x2");
            }

            return hashedpass;
        }

        public CommandItem getUser(string Username)
        {
            try
            {
                var j = (from c in data.CommandItems
                         where (c.username == Username)
                         select c).OrderByDescending(c => c.__createdAt);

                lbldb.Text = "From DB : " + count++ + " : " + j.First();
                return j.First();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        public void PostComputerName(string UserName, CommandItem update)
        {
            try
            {
                CommandItem c = getUser(UserName);

                c.computername = update.computername;

                data.SubmitChanges();

            }
            catch (Exception)
            {
            }
        }


        public void GenerateProcessList()
        {
            Process[] localAll = Process.GetProcesses();

            File.WriteAllText(@"C:/Users/George/Desktop/" + txtUname.Text + "processlist.txt", "" + Environment.NewLine);

            for (int i = 0; i < localAll.Count(); i++)
            {

                File.AppendAllText(@"C:/Users/George/Desktop/" + txtUname.Text + "processlist.txt", localAll.GetValue(i).ToString() + Environment.NewLine);
            }

        }

        public string getCommand(string Username)
        {
            try
            {
                var j = (from c in data.CommandItems
                         where (c.username == Username)
                         select c).OrderByDescending(c => c.__createdAt);

                lbldb.Text = "From DB : " + count++ + " : " + j.First().command;
                return j.First().command;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string getProcess(string Username)
        {
            try
            {
                var j = (from c in data.CommandItems
                         where (c.username == Username)
                         select c).OrderByDescending(c => c.__createdAt);

                lbldb.Text = "From DB : " + count++ + " : " + j.First().terminateprocess;
                return j.First().terminateprocess;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public bool updateCommand(string UserName, CommandItem update)
        {
            try
            {
                CommandItem c = getUser(UserName);

                c.command = update.command;

                data.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool updateTerminate(string UserName, CommandItem update)
        {
            try
            {
                CommandItem c = getUser(UserName);

                c.terminateprocess = update.terminateprocess;

                data.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private void cmdLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void TerminateProc(string procname)
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName(procname))
                {
                    proc.Kill();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdBlob_Click(object sender, EventArgs e)
        {
            //GenerateProcessList();
            //uploadBlob();
            //downloadBlob();
            // ExecuteCommand("TERMINATE Everything");
        }


    }
}
