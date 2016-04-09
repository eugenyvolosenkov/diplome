using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.FreeGlut;
using Tao.OpenGl;
using Vector;
namespace LibInstrument
{
    class Camera
    {
       private Vector3D ICamLocation;
       private Vector3D ICamFocus;
       private Vector3D ICamErros;
       private bool IsUsing = false;
       public Camera()
       {
           ICamLocation = Vector3D.Zero;
           ICamFocus = Vector3D.Zero;
           ICamErros = Vector3D.Zero;
       }
       public Camera(Vector3D Location, Vector3D Focus, Vector3D Erros)
       {
           ICamLocation = Location;
           ICamFocus = Focus;
           ICamErros = Erros;
       }
       public void Reload(Vector3D Location, Vector3D Focus, Vector3D Erros)
        {
            ICamLocation = Location;
            ICamFocus = Focus;
            ICamErros = Erros;
        }
       public void RemoveLoc(Vector3D Location)
       {
           ICamLocation = Location;
       }
       public void RemoveFoc(Vector3D Focus)
       {
           ICamFocus = Focus;
       }
       public void ReErros(Vector3D Erros)
       {
           ICamErros = Erros;
       }
       public void SwitchOn()
       {
           IsUsing = true;
       }
       public void SwitchOff()
       {
           IsUsing = false;
       }
        public void Motion()
        {
         //   Glu.gluLookAt();
        }
    }
}
