using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinipulVS
{
    public interface IMessageServise
    {
        void ShowError(string error);
    }

    class MessageServise : IMessageServise
    {
        public void ShowError(string error)
        {
            MessageBox.Show(error, "Ошибка приложения", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
