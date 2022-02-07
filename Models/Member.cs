using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace TALExamMVC.Models
{
    public class Member
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public short Age { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; } = DateTime.Now;
        [Required]
        public string Occupation { get; set; }
        public List<Occupation> OccupationList { get; set; }
        [Required]
        public double SumInsured { get; set; }
        [DataType(DataType.Currency)]
        public double MonthlyPremium { get; set; } 

        public Member()
        {
            OccupationList = PopulateOccupation();
        }

        private SelectList ToSelectList(List<Occupation> occupations)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var row in occupations)
            {
                list.Add(new SelectListItem()
                {
                    Text = row.Title,
                    Value = row.Rating.ToString()
                }); ;
            }

            return new SelectList(list, "Value", "Text");
        }

        private List<Occupation> PopulateOccupation()
        {
            List<Occupation> occupations = new List<Occupation>();
            XDocument xmlDoc = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/Occupations.xml"));

            //var model = (from xml in xmlDoc.Descendants("Occupation")
            //             select xml) as IEnumerable<Occupation>;
            //var serializer = new XmlSerializer(typeof(Occupation));
            //model =
            //    from xml in xmlDoc.Descendants("Occupations")
            //    select serializer.Deserialize(xml.CreateReader()) as EmployeesModel;

            var model = from a in xmlDoc.Descendants("Occupation")
                        select new Occupation
                        {
                            Title = a.Element("Title").Value,
                            //Type = a.Element("Type").Value,
                            Rating = Convert.ToDouble(a.Element("Rating").Value)
                        };

            foreach (Occupation occupation in model)
            {
                occupations.Add(occupation);
            }

            return occupations;
        }

    }
}