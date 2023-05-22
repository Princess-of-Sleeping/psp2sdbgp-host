using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PSP2TMAPILib;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public unsafe Form1()
        {
            InitializeComponent();

            tm = new PSP2TMAPI();
            tm.CheckCompatibility((uint)eCompatibleVersion.BuildVersion);

            tgt = tm.Targets.DefaultTarget;

            uint a1, a2, a3;

            tgt.ProtocolAvailabilityInfo(0x10020000, out a1, out a2, out a3);

            tgt.RegisterCustomProtocol(0x10020000); // SDBGP0
            tgt.RegisterCustomProtocol(0x10020001); // SDBGP1
            tgt.RegisterCustomProtocol(0x10020002); // SDBGP2
            tgt.RegisterCustomProtocol(0x10020003); // SDBGP3
        }

        private PSP2TMAPI tm;
        private ITarget tgt;

        private unsafe void button1_Click(object sender, EventArgs e)
        {
            byte[] b2 = new byte[0x24];

            b2[0] = 0;
            b2[1] = 0xF0;
            b2[2] = 0xA; // sceKernelSnapshot
            b2[2] = 8; // kshow
            b2[3] = 0;

            int kshow = (int)numericUpDown1.Value;

            b2[4] = (byte)(kshow & 0xFF);
            b2[5] = (byte)((kshow >> 8) & 0xFF);
            b2[6] = (byte)((kshow >> 16) & 0xFF);
            b2[7] = (byte)((kshow >> 24) & 0xFF);

            b2[8] = 0;
            b2[9] = 0;
            b2[0xA] = 0;
            b2[0xB] = 0;
            b2[0xC] = 0;
            b2[0xD] = 0;
            b2[0xE] = 0;
            b2[0xF] = 0;
            b2[0x10] = 0;
            b2[0x11] = 0;
            b2[0x12] = 0;
            b2[0x13] = 0;

            try
            {
                tgt.SendCustomProtocolData(0x10020000, b2);
                // tgt.SendCustomProtocolData(0x10020001, b2);
                // tgt.SendCustomProtocolData(0x10020002, b2);
                // tgt.SendCustomProtocolData(0x10020003, b2);
            }
            catch (ArgumentException excp)
            {
                Console.WriteLine("{0}: {1}", excp.GetType().Name, excp.Message);
                Console.WriteLine("{0}", excp.ParamName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] b2 = new byte[0x24];

            b2[0] = 0;
            b2[1] = 0xF0;
            b2[2] = 0xA; // sceKernelSnapshot
            b2[3] = 0;

            // b2[4] = 2;
            // b2[4] = 4;
            b2[4] = 1;
            b2[5] = 0;
            b2[6] = 0;
            b2[7] = 0;

            b2[8] = 0;
            b2[9] = 0;
            b2[0xA] = 0;
            b2[0xB] = 0;
            b2[0xC] = 0;
            b2[0xD] = 0;
            b2[0xE] = 0;
            b2[0xF] = 0;
            b2[0x10] = 0;
            b2[0x11] = 0;
            b2[0x12] = 0;
            b2[0x13] = 0;

            try
            {
                // tgt.SendCustomProtocolData(0x10020000, b);
                tgt.SendCustomProtocolData(0x10020000, b2);
                // tgt.SendCustomProtocolData(0x10020001, b2);
                // tgt.SendCustomProtocolData(0x10020002, b2);
                // tgt.SendCustomProtocolData(0x10020003, b2);
            }
            catch (ArgumentException excp)
            {
                Console.WriteLine("{0}: {1}", excp.GetType().Name, excp.Message);
                Console.WriteLine("{0}", excp.ParamName);
            }
        }
    }
}
