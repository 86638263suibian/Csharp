using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HttpCodeLib;
using System.Threading;


namespace DZloging
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            Thread th = new Thread(Getform); 
            th.Start();

        }
        
        //作者小哲QQ839544278
        //Blog www.test404.com

        XJHTTP xj = new XJHTTP();
        HttpHelpers helper = new HttpHelpers();//请求执行对象
        HttpItems items;//请求参数对象
        HttpResults hr = new HttpResults();//请求结果对象
        string StrCookie = "";//设置初始Cookie值
        string seccode;//idhash
        string formhash;//提交表单
        string loginhash;//
 
        public void Getform()
        {
            string res = string.Empty;//请求结果,请求类型不是图片时有效
            string url = "http://bbs.itxueke.com/member.php?mod=logging&action=login";//请求地址
            items = new HttpItems();//每次重新初始化请求对象
            items.URL = url;//设置请求地址
            items.Referer = "http://bbs.itxueke.com/member.php?mod=logging&action=login";//设置请求来源
            items.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:17.0) Gecko/20100101 Firefox/17.0";//设置UserAgent
            items.Cookie = StrCookie;//设置字符串方式提交cookie
            items.Allowautoredirect = true;//设置自动跳转(True为允许跳转) 如需获取跳转后URL 请使用 hr.RedirectUrl
            items.ContentType = "application/x-www-form-urlencoded";//内容类型
            hr = helper.GetHtml(items, ref StrCookie);//提交请求
            string html = hr.Html;//具体结果
            //return html;//返回具体结果
            seccode = xj.GetStringMid(html, "seccode_", "\"");
            loginhash = xj.GetStringMid(html, "loginhash=", "\"");
            formhash = xj.GetStringMid(html, "<input type=\"hidden\" name=\"formhash\" value=\"", "\"");
            Console.WriteLine("{0},{1},{2}", seccode, loginhash, formhash);
            Console.WriteLine(hr.Cookie);
            GetCode();
        }//获取登录相关参数
        
        private string Getupdate()//获取验证码地址
        {
            string res = string.Empty;//请求结果,请求类型不是图片时有效
            string url = "http://bbs.itxueke.com/misc.php?mod=seccode&action=update&idhash=" + seccode;//请求地址
            items = new HttpItems();//每次重新初始化请求对象
            items.URL = url;//设置请求地址
            items.Referer = "http://bbs.itxueke.com/member.php?mod=logging&action=login";//设置请求来源
            items.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:17.0) Gecko/20100101 Firefox/17.0";//设置UserAgent
            items.Cookie = StrCookie;//设置字符串方式提交cookie
            items.Allowautoredirect = true;//设置自动跳转(True为允许跳转) 如需获取跳转后URL 请使用 hr.RedirectUrl
            items.ContentType = "application/x-www-form-urlencoded";//内容类型
            hr = helper.GetHtml(items, ref StrCookie);//提交请求
            res = hr.Html;//具体结果
            string update = xj.GetStringMid(res, "width=\"100\" height=\"30\" src=\"", "\"");
            Console.WriteLine(update);
            Console.WriteLine(hr.Cookie);
            return update;//返回具体结果
            
        }

        private Image GetImage(string update)
        {
            string url = "http://bbs.itxueke.com/" + update;//请求地址
            items = new HttpItems();//每次重新初始化请求对象
            items.URL = url;//设置请求地址
            items.Referer = "http://bbs.itxueke.com/member.php?mod=logging&action=login";//设置请求来源
            items.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:17.0) Gecko/20100101 Firefox/17.0";//设置UserAgent
            items.Cookie = StrCookie;//设置字符串方式提交cookie
            items.Allowautoredirect = true;//设置自动跳转(True为允许跳转) 如需获取跳转后URL 请使用 hr.RedirectUrl
            items.ContentType = "application/x-www-form-urlencoded";//内容类型
            items.ResultType = ResultType.Byte;//设置返回结果为byte
            hr = helper.GetHtml(items, ref StrCookie);//提交请求
            Console.WriteLine(hr.Cookie);
            return helper.GetImg(hr);//获取图片
            //调用示例:  picImage.Image = HttpCodeGetImage(); picImage 为图片控件名
        }//获取验证码图片
   
        private void GetCode()
        {
            string update = Getupdate();
            pictureBox1.Image = GetImage(update);

        }//获取验证码到控件

        private void GetLogin()
        {
            string res = string.Empty;//请求结果,请求类型不是图片时有效
            string url = "http://bbs.itxueke.com/misc.php?mod=seccode&action=check&inajax=1&modid=member::logging&idhash=" + seccode + "&secverify=" + textBoxCode.Text;//请求地址
            items = new HttpItems();//每次重新初始化请求对象
            items.URL = url;//设置请求地址
            items.Referer = "http://bbs.itxueke.com/member.php?mod=logging&action=login";//设置请求来源
            items.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:17.0) Gecko/20100101 Firefox/17.0";//设置UserAgent
            items.Cookie = StrCookie;//设置字符串方式提交cookie
            items.Allowautoredirect = true;//设置自动跳转(True为允许跳转) 如需获取跳转后URL 请使用 hr.RedirectUrl
            items.ContentType = "application/x-www-form-urlencoded";//内容类型
            hr = helper.GetHtml(items, ref StrCookie);//提交请求
            res = hr.Html;//具体结果
            if (res.IndexOf("succeed")==-1)
            {
                MessageBox.Show("抱歉，验证码填写错误,请刷新验证码！");
                return;
            }
            Console.WriteLine(res);
            Console.WriteLine(hr.Cookie);

            res = string.Empty;//请求结果,请求类型不是图片时有效
            url = "http://bbs.itxueke.com/member.php?mod=logging&action=login&loginsubmit=yes&loginhash" + loginhash + "&inajax=1";//请求地址
            items = new HttpItems();//每次重新初始化请求对象
            items.URL = url;//设置请求地址
            items.Referer = "http://bbs.itxueke.com/member.php?mod=logging&action=login";//设置请求来源
            items.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:17.0) Gecko/20100101 Firefox/17.0";//设置UserAgent
            items.Cookie = StrCookie;//设置字符串方式提交cookie
            items.Allowautoredirect = true;//设置自动跳转(True为允许跳转) 如需获取跳转后URL 请使用 hr.RedirectUrl
            items.ContentType = "application/x-www-form-urlencoded";//内容类型
            items.Method = "POST";//设置请求数据方式为Post
            items.Postdata = "formhash=" + formhash + "&referer=http%3A%2F%2Fbbs.itxueke.com%2F.%2F&loginfield=username&username=" + textBoxUers.Text + "&password=" +xj.UrlEncoding(textBoxPass.Text) + "&questionid=0&answer=&seccodehash=" + seccode + "&seccodemodid=member%3A%3Alogging&seccodeverify=" + textBoxCode.Text;//Post提交的数据
            hr = helper.GetHtml(items, ref StrCookie);//提交请求
            res = hr.Html;//具体结果
            
            //Console.WriteLine(res);
            string temp;
            if (res.IndexOf("errorhandle") == -1)
            {
                temp = xj.GetStringMid(res, "欢迎您回来，", "，");
                
            }
            else
            {
                temp = xj.GetStringMid(res, "CDATA[", "<");
                
            }
            MessageBox.Show(temp);
            
        }//开始登录

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.DoEvents();//处理事件
            GetCode();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            try
            {
                GetLogin();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            xj.OpenUrl("http://bbs.itxueke.com/", 1);
        }
    }
}
