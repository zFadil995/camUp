using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CamUp
{
    public interface IResizeImage
    {
        byte[] ResizeImage(byte[] imageData);
    }

}
