using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Diagnostics;
namespace cidr_calculator
{
    public class ClasslessInterDomainRouting
    {
        public double MaximumSubnets { get; set; }
        public double MaximumAddresses { get; set; }
        public string Wildcard { get; set; }
        
        public int FirstOctetWildcard { get; set; }
        public int SecondOctetWildcard { get; set; }
        public int ThirdOctetWildcard { get; set; }
        public int FourthOctetWildcard { get; set; }

        public ClasslessInterDomainRouting(){}

        public ClasslessInterDomainRouting(string Decimal) {
            this.Decimal = Decimal;
            this.Binary32 = ToBinary32();
        }

        public ClasslessInterDomainRouting(string Notation, string Decimal, string Binary32)
        {
            this.Notation = Notation;
            this.Decimal = Decimal;
            this.Binary32 = Binary32;

        }

        public string WildcardBinary
        {
            get
            {
                string temp = Convert.ToString(FirstOctetWildcard, 2).PadLeft(8, '0');
                temp += "." + Convert.ToString(SecondOctetWildcard, 2).PadLeft(8, '0');
                temp += "." + Convert.ToString(ThirdOctetWildcard, 2).PadLeft(8, '0');
                temp += "." + Convert.ToString(FourthOctetWildcard, 2).PadLeft(8, '0');
                return temp;
            }

            set { this.WildcardBinary = value; }
        }


        public ClasslessInterDomainRouting(string Notation, string Decimal)
        {
            this.Notation = Notation;
            this.Decimal = Decimal;
            this.Binary32 = ToBinary32();

            int netmaskNotation = Convert.ToInt32(Notation.Substring(1));

            string mask = "";
            for (int i = 0; i < netmaskNotation; i++)
            {
                mask += "1";
            }
            mask = mask.PadRight(32, '0');

            Debug.WriteLine("netmask notation " + netmaskNotation);
            MaximumSubnets = Math.Pow(2,(32 - netmaskNotation));
            MaximumAddresses = MaximumSubnets -2;
            if (netmaskNotation == 32) MaximumAddresses = 1;
            if (netmaskNotation == 31) MaximumAddresses = 2;

            Debug.WriteLine("mask " + mask);
            Debug.WriteLine("decimal " + Decimal);
            Debug.WriteLine("max subnets " + MaximumSubnets);
            Debug.WriteLine("max addresses " + MaximumAddresses);

            FirstOctetWildcard = 255 - Convert.ToInt32(FirstOctetDecimal);
            SecondOctetWildcard = 255 - Convert.ToInt32(SecondOctetDecimal);
            ThirdOctetWildcard = 255 - Convert.ToInt32(ThirdOctetDecimal);
            FourthOctetWildcard = 255 - Convert.ToInt32(FourthOctetDecimal);

            Wildcard = FirstOctetWildcard + "." + SecondOctetWildcard + "." + ThirdOctetWildcard + "." + FourthOctetWildcard;


            Debug.WriteLine("wildcard mask " + Wildcard);


            //http://www.ehow.com/how_5195438_calculate-subnet-address.html
            Debug.WriteLine("subnet ip ");

            //int first = Convert.ToInt32(FirstOctetDecimal) - 255;
            //int second = Convert.ToInt32(SecondOctetDecimal) - 255;
            //int third = Convert.ToInt32(ThirdOctetDecimal) - 255;
            //int fourth = Convert.ToInt32(FourthOctetDecimal) - 255;
            // MaximumSubnets = 
        }

        public ClasslessInterDomainRouting(string Notation, string Decimal, int MaximumSubnets, int MaximumAddresses)
        {
            this.Notation = Notation;
            this.Decimal = Decimal;
            this.Binary32 = ToBinary32();
            this.MaximumSubnets = MaximumSubnets;
            this.MaximumAddresses = MaximumAddresses;
        }


        //public ClasslessInterDomainRouting(string Notation, string Binary32)
        //{
        //    this.Notation = Notation;
        //    this.Binary32 = Binary32;
        //    this.Decimal = ToDecimal();
        //}




        public string ToBinary32()
        {
            int mBase = 2;
            string binary ="";
            IPAddress ip = IPAddress.Parse(Decimal);
            foreach (byte octet in ip.GetAddressBytes())
            {
                binary += Convert.ToString(octet,mBase).PadLeft(8,'0');
            }
            return binary;
        }

        public string ToDecimal()
        {
            return Convert.ToInt32(FirstOctetBinary, 2).ToString() +"." +
                    Convert.ToInt32(SecondOctetDecimal, 2).ToString() + "." +
                    Convert.ToInt32(ThirdOctetDecimal, 2).ToString() + "." +
                    Convert.ToInt32(FourthOctetDecimal, 2).ToString();
        }

        public string Notation
        {
            get;
            set;
        }

        public string Decimal
        {
            get;
            set;
        }

        public string Binary32
        {
            get;
            set;
            
        }

        

        public string FirstOctetDecimal
        {
            get {
                IPAddress ip = IPAddress.Parse(Decimal);
                byte[] temp = ip.GetAddressBytes();
                return temp[0].ToString();
               // return Convert.ToInt32( Binary32.Substring(0, 8) , 2).ToString();
            }
            set { this.FirstOctetDecimal = value; }
        }


        public string SecondOctetDecimal
        {
            get
            {
                IPAddress ip = IPAddress.Parse(Decimal);
                byte[] temp = ip.GetAddressBytes();
                return temp[1].ToString();
                //return Convert.ToInt32(Binary32.Substring(8, 8), 2).ToString();
            }
            set { this.SecondOctetDecimal = value; }
        }

        public string ThirdOctetDecimal
        {
            get
            {
                IPAddress ip = IPAddress.Parse(Decimal);
                byte[] temp = ip.GetAddressBytes();
                return temp[2].ToString();
                //return Convert.ToInt32(Binary32.Substring(16, 8), 2).ToString();
            }
            set { this.ThirdOctetDecimal = value; }
        }

        public string FourthOctetDecimal
        {
            get
            {
                IPAddress ip = IPAddress.Parse(Decimal);
                byte[] temp = ip.GetAddressBytes();
                return temp[3].ToString();
                //return Convert.ToInt32(Binary32.Substring(24, 8), 2).ToString();
            }
            set { this.FourthOctetDecimal = value; }
        }


        public string FirstOctetBinary
        {
            get {
                    //int mBase = 2;
                    //IPAddress ip = IPAddress.Parse(Decimal);
                    //byte[] temp = ip.GetAddressBytes();
                    //return Convert.ToString(temp[0], mBase).PadLeft(8, '0');
                return Binary32.Substring(0, 8);
            }

            set { this.FourthOctetBinary = value; }
        }


        public string SecondOctetBinary
        {
            get
            {
                //int mBase = 2;
                //IPAddress ip = IPAddress.Parse(Decimal);
                //byte[] temp = ip.GetAddressBytes();
                //return Convert.ToString(temp[1], mBase).PadLeft(8, '0');
               return Binary32.Substring(8, 8);
            }
            set { this.SecondOctetBinary = value; }
        }

        public string ThirdOctetBinary
        {
            get
            {
                //int mBase = 2;
                //IPAddress ip = IPAddress.Parse(Decimal);
                //byte[] temp = ip.GetAddressBytes();
                //return Convert.ToString(temp[2], mBase).PadLeft(8, '0');
                return Binary32.Substring(16, 8);
            }
            set { this.ThirdOctetBinary = value; }
        }

        public string FourthOctetBinary
        {
            get
            {
                //int mBase = 2;
                //IPAddress ip = IPAddress.Parse(Decimal);
                //byte[] temp = ip.GetAddressBytes();
                //return Convert.ToString(temp[3], mBase).PadLeft(8, '0');
                return Binary32.Substring(24, 8);
            }
            set { this.FourthOctetBinary = value; }
        }



    }
}
