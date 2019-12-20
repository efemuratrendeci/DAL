using Libraries.DataAcces.Core.Model.ADO.Net_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GetData();
        }

        static void GetData()
        {
            using (TransactionScope scope = new TransactionScope("Server=.;Database=V3;User Id=sa;Password=1;", true))
            {
                ExecuteQueryParametersModel param = new ExecuteQueryParametersModel()
                {
                     Name = "@CurrAccCode",
                     StringValue = "1-4-1"
                };
                ExecuteQueryModel model = new ExecuteQueryModel()
                {
                    QueryText = "UPDATE dbo.cdCurrAcc SET FirstName = 'Transaction' WHERE CurrAccCode = @CurrAccCode"
                };
                model.Parameters.Add(param);
                var a = scope.Select(model);
            }
        }

    }
}
