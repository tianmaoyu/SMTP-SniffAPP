using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Async_Await
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            this.textBox1.Text += ":主线程开始运行\r";

           // Does();
            DoWork();

            this.textBox1.Text += ":主线程已经结束\r";
           
        }
        static async Task<String> AsyncMethod()
        {
            var result = await MyMethod();
            return ":异步运行5秒后回来了\r";
        }
        static async Task<int> MyMethod()
        {
            for(int i = 0; i < 5; i++)
            {
                await Task.Delay(1000);
            }
            return 0;
        }
        private Task<string> DoWork()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(4000);
                //在线程中该控件
                this.Invoke((Action)delegate { this.textBox1.Text = "ddd"; });
                Thread.Sleep(4000);
                return "Done with work!";
            }
          );
        }
        private async void Does()
        {
            string text = await DoWork();
            textBox1.Text = text;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = await DoWork();
            Task.Factory.StartNew(() => {
                proccess();
            });
        }
        public async Task<string> DoWorkAsync(string dd)
        {
            return await Task.Run(() =>
            {
                return "dd";
            });
        }
        //并行处理
        private void proccess()
        {
            List<string> list = new List<string>();
            Parallel.ForEach(list, item =>
            {
            //处理
            this.Invoke((Action)delegate{
                this.textBox1.Text = "ddd";
            });
            });
            //并行处理
            Parallel.Invoke(()=> { },()=> { });
        }

        //返回空方法
        private async Task ddd()
        {
            await Task.Run(() =>
            {

            });
        }

        //
        private async Task<int> Sudm(string da)
        {
            //两个return
           return await Task.Run(() =>
            {
                if (da==null)
                {
                    return 3;
                }
                else
                {
                    return 2;
                }
                return 0;
            });
        }
    }
}
