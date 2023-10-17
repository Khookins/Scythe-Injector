using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scythe
{
    public partial class label : Component
    {
        public label()
        {
            InitializeComponent();
        }

        public label(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
