using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.isp
{
    internal class Case2
    {
        // Узкие интерфейсы
        // Звонки
        public interface ICallable
        {
            void Call(string number);
        }

        // Поиск
        public interface IBrowsable
        {
            void Browse(string url);
        }

        // Фото
        public interface IPhotographable
        {
            void TakePhoto();
        }

        // Почта
        public interface IEmailSendable
        {
            void SendEmail(string recipient, string subject, string body);
        }

        // Плеер
        public interface IMusicPlayable
        {
            void PlayMusic();
        }

        // Смартфон поддерживает все функции
        public class SmartPhone : ICallable, IBrowsable, IPhotographable, IEmailSendable, IMusicPlayable
        {
            public string Model { get; set; }
            public string OS { get; set; }

            public SmartPhone(string model, string os)
            {
                Model = model;
                OS = os;
            }

            public void Call(string number)
            {
                Console.WriteLine("SmartPhone " + Model + " calling "+ number);
            }

            public void Browse(string url)
            {
                Console.WriteLine("SmartPhone " + Model + " browsing " + url);
            }

            public void TakePhoto()
            {
                Console.WriteLine("SmartPhone " + Model + " takes a high quality photo");
            }

            public void SendEmail(string recipient, string subject, string body)
            {
                Console.WriteLine("SmartPhone " + Model + " sending email to " + recipient);
            }

            public void PlayMusic()
            {
                Console.WriteLine("SmartPhone " + Model + " is playing music");
            }
        }

        // Базовый телефон поддерживает только звонки, фото и SMS (но SMS не входит в интерфейсы)
        public class BasicPhone : ICallable, IPhotographable
        {
            public string Model { get; set; }

            public BasicPhone(string model)
            {
                Model = model;
            }

            public void Call(string number)
            {
                Console.WriteLine("BasicPhone " + Model + " calling " + number);
            }

            public void TakePhoto()
            {
                Console.WriteLine("BasicPhone " + Model + " takes a very low quality photo");
            }

            // Базовый телефон может только в смс, не в почту
            public void SendSMS(string recipient, string message)
            {
                Console.WriteLine("BasicPhone " + Model + " sending SMS to " + recipient);
            }
        }
    }
}
