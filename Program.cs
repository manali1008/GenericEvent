using GenericEvent.EventsExample;
using GenericEvent.GenericExample;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericEvent
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Event Example
            //Start
            ResumeUploaded resumeUploaded = new ResumeUploaded();
            Applicant applicant = new Applicant();
            applicant.name = "Jacob";

            resumeUploaded.ResumeUploadEvent += applicant.HandleResumeUploaded;

            resumeUploaded.Uploaded();
            //End


            //Generic Example
            //Start
            CustomCollection<int> intCollection = new CustomCollection<int>();
            intCollection.Add(101);
            intCollection.Add(113);
            intCollection.Add(125);
            intCollection.Add(134);
            intCollection.Add(147);

            Console.WriteLine($"Item at 2 : {intCollection.Get(2)}");
            Console.WriteLine($"Item at 5 : {intCollection.Get(5)}");

            Console.WriteLine($"List in reverse order");

            List<int> reverseList = intCollection.SortedList();

            foreach (int item in reverseList)
            {
                Console.WriteLine(item);
            }

            //Not allowed to create refence type
            //CustomCollection<string> strCollection = new CustomCollection<string>();

            //End
        }
    }

    namespace EventsExample
    {
        public class Applicant
        {
            public string name { get; set; }

            public void HandleResumeUploaded(object sender, ResumeUploadEventArgs e)
            {
                Console.WriteLine($"Resume uploaded for {name}");
            }
        }
        public class ResumeUploaded
        {
            public event ResumeUploadEventHAndler ResumeUploadEvent;

            public void Uploaded()
            {
                if(ResumeUploadEvent != null)
                {
                    ResumeUploadEvent(this, new ResumeUploadEventArgs("Resume Uploaded"));
                }
            }
        }

        public delegate void ResumeUploadEventHAndler(object source, ResumeUploadEventArgs e);
        public class ResumeUploadEventArgs : EventArgs
        {
            public string applicant { get; set; }
            public ResumeUploadEventArgs(string appl)
            {
                this.applicant = appl;
            }
        }

    }

    namespace GenericExample
    {
        public class CustomCollection<T> where T: struct
        {
            private List<T> collection = new List<T>();

            public void Add(T item)
            {                
                collection.Add(item);
            }

            public T Get(int index)
            {
                if (index < collection.Count)
                    return collection[index];
                else
                    return collection[0];
            }

            public List<T> SortedList()
            {
                return collection.OrderByDescending(i => i).ToList();
            }

        }
    }
}
