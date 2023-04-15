using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram
{
    public class Contact
    {
        public string Name { get; set; }
        public string MessagePreview { get; set; }

        //添加时间
        public DateTime DateTime { get; set; }
    }

}
