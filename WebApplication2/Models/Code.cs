using System;
using System.ComponentModel;
namespace WebApplication2.Models
{
    public class Code
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [DisplayName("Code Languge")]
        public string CodeLanguge { get; set; }
        [DisplayName("Description")]
        public string Descreption { get; set; }
        [DisplayName("Code Deposit")]
        public string CodeDeposit { get; set; }
        public DateTime date { get; set; }
        public string UserId { get; set; }
        public bool Hiden { get; set; }
    }

}

