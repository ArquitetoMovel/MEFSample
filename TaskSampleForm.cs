using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskPoc
{
    public partial class TaskSampleForm : Form
    {
        private Stopwatch _crono;
        private Task _work;
        private CancellationTokenSource _tokenSource;
        //private Thread _tWork;

        public TaskForm()
        {
            InitializeComponent();
            _crono = new Stopwatch();
        }

        private void StartTask_Click(object sender, EventArgs e)
        {

            _tokenSource = new CancellationTokenSource();
            var token = _tokenSource.Token;
            listInfo.Items.Add("begin ");
            //_tWork = new Thread(DoWork);
            //_tWork.IsBackground = true;
            //_tWork.Name = "Wrk";
            //_tWork.Start();
           
            _work = Task.Factory.StartNew(() => DoWork(), token);
            
        }

        private  void DoWork()
        {
            while (true)
            {
                _crono.Start();
                var info = $"executou {Thread.CurrentThread.Name}! ";
                //await Task.Delay(0).ConfigureAwait(false);
                try
                {
                    _work.Wait(1000, _tokenSource.Token);
                }catch(OperationCanceledException){
                    // ex eperada
                }
                
                //Thread.Sleep(1000);
                _crono.Stop();

                listInfo.Invoke(new MethodInvoker(delegate
                {
                    listInfo.Items.Add($"{info} em {_crono.ElapsedMilliseconds} ms");
                }));

                _crono.Reset()

                if (_tokenSource.IsCancellationRequested)
                {
                    _tokenSource.Dispose();
                    break;
                }

            }
        }

        private void StopTask_Click(object sender, EventArgs e)
        {

            listInfo.Items.Add("cancel request");            
            _tokenSource.Cancel();
        }
    }
}
