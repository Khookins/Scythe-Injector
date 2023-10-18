using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Scythe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Process[] PC = Process.GetProcesses().Where(p => (long)p.MainWindowHandle != 0).ToArray();
            comboBox1.Items.Clear();
            foreach (Process p in PC)
            {
                comboBox1.Items.Add(p.ProcessName);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private static string dllp { get; set; }
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = @"C:\";
                ofd.Title = "Select DLL File For Injection";
                ofd.DefaultExt = "dll";
                ofd.Filter = "DLL Files (*.dll|*.dll)";
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;
                ofd.ShowDialog();

                textBox1.Text = ofd.FileName;
                dllp = ofd.FileName;
            }

            catch (Exception ed)
            {
                MessageBox.Show(ed.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dllp = textBox1.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process[] PC = Process.GetProcesses().Where(p => (long)p.MainWindowHandle != 0).ToArray();
            comboBox1.Items.Clear();
            foreach (Process p in PC)
            {
                comboBox1.Items.Add(p.ProcessName);
            }
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        static readonly IntPtr INTPTR_ZERO = (IntPtr)0;
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr OpenProcess(uint dwDesiredAcces, int bInheritHandle, uint dwProcessId);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int CloseHandle(IntPtr hObject);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, uint flAllocationType, uint flProtect);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, uint size, int lpNumberOfBytesWritten);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttribute, IntPtr dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        public static int Inject(string pn, string dllp)
        {
            // 1 = File Does Not Exist / File Is Null
            // 2 = Inactive Process
            // 3 = Injection Failed
            // 4 = Injection Succeded

            if (!File.Exists(dllp)) return 1;

            uint _procId = 0;
            Process[] _procs = Process.GetProcesses();
            for (int i = 0; i < _procs.Length; i++)
            {
                if (_procs[i].ProcessName == pn)
                {
                    _procId = (uint)_procs[i].Id;
                }
            }
            if (_procId == 0) return 2;

            if (!SI(_procId, dllp)) return 3;
            return 4;
        }

        public static bool SI(uint p, string ddlp)
        {
            IntPtr hndProc = OpenProcess((0x2 | 0x8 | 0x10 | 0x20 | 0x400), 1, p);
            if (hndProc == INTPTR_ZERO) return false;
            IntPtr lpAddress = VirtualAllocEx(hndProc, (IntPtr)null, (IntPtr)dllp.Length, (0x1000 | 0x2000), 0x40);
            if (lpAddress == INTPTR_ZERO)return false;
            byte[] bytes = Encoding.ASCII.GetBytes(ddlp);

            if (WriteProcessMemory(hndProc, lpAddress, bytes, (uint)bytes.Length, 0).ToInt32() == 0) return false;
            CloseHandle(hndProc);
            return true;          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int Result = Inject(comboBox1.Text, dllp);

            if (Result == 1)
            {
                MessageBox.Show("File Doesn't Exist / No DLL Selected");
            }
            if (Result == 2)
            {
                MessageBox.Show("No Process Selected");
            }
            if (Result == 3)
            {
                MessageBox.Show("Injection Failed");
            }
            if (Result == 4)
            {
                MessageBox.Show("Injection Succeeded");
            }
        }
    }

}
 