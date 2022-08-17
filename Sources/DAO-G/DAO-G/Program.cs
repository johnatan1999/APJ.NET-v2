using Apj.Net.Dao;
using Apj.Net.Dao.Common;
using Apj.Net.Dao.Common.Query;
using Apj.Net.Dao.Model;
using Apj.Net.Dao.Util;
using System;
using System.Collections.Generic;

namespace DAO_G
{
    class Program
    {
        static void Main(string[] args)
        {
            IApjDao dao = ApjDaoFactory.GetInstance();
            if(dao != null)
            {
                IList<ApjTest> apjTests = dao.FindAll<ApjTest>();
                Console.WriteLine("ApjTest count: " + apjTests.Count);
                ApjTest test = new ApjTest();
                test.Name = "Kono";
                test.Description = "Yaro";
                QueryBuilder<ApjTest> qb = new QueryBuilder<ApjTest>();
                qb.Select("Name")
                    .WhereStartsWith("Name", "Effacer ")
                    .OrderBy("Id")
                    .Desc();
                Console.WriteLine("QB " + qb);
                IList<ApjResult> res = qb.Execute();
                Console.WriteLine("res.length " + res.Count);
                IList<ApjTest> res2 = SearchQuery.findWhere<ApjTest>(
                    Criteria.Eq("Id", "APJ0000028 ")
                    );
                Console.WriteLine("res2.length " + res2.Count);
                res2 = SearchQuery.findWhere<ApjTest>("Id = 'APJ0000028'");
                Console.WriteLine("res3.length " + res2.Count);

                res2 = dao.FindAll<ApjTest>();
                Console.WriteLine("res3.length " + res2.Count);
                foreach(ApjTest a in res2)
                {
                    Console.WriteLine(a.Id);
                }
                dao.Add(test);
                test.Id = "APJ0000025";
                test.Load();
                Console.WriteLine("__" + test.Description);
                res2 = SearchQuery.findWhere<ApjTest>(Criteria.Eq("name", "Ayoo"));
                Console.WriteLine("res4.length " + res2.Count);


                Console.WriteLine("Search criteria builder");
                Dictionary<string, object> p = new Dictionary<string, object>();
                //p.Add("name", "Ay");
                p.Add("state-1", 1);
                //p.Add("state-2", 8);
                List<Criteria> criteria = ApjDaoUtil.MakeSearchCriteria(typeof(ApjTest), p);
                foreach(var c in criteria)
                {
                    Console.WriteLine("criteria="+c.ToString());
                }
                res2 = SearchQuery.findWhere<ApjTest>(criteria.ToArray());
                Console.WriteLine("res5.length " + res2.Count);
                //dao.Update(test);
                //dao.Delete(test);
            }
        }
    }
}
