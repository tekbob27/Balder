#if(SILVERLIGHT)
using System.Windows.Media;
#else
using System.Drawing;
#endif
using Balder.Core.Math;
using Balder.Core.Extensions;


namespace Balder.Core.Lighting
{
    public class DirectionalLight : Light
    {
        public float Strength;
        public bool Specular;
        public bool Diffuse;
        public bool Ambient;
        public Vector Direction;

        public DirectionalLight()
		{
			Strength = 1f;
            Specular = true;
            Diffuse = true;
            Ambient = true;
            Direction = new Vector(0, 0, 1, 0);
		}

		public override Color Calculate(Vector point, Vector normal)
		{
            // Use dotproduct for diffuse lighting. Add point functionality as this now is a directional light.
            // Ambient light
            var ambient = ColorAmbient.ToVector() * Strength;

            // Diffuse light
            var lightDir = Direction;
            lightDir.Normalize();
            normal.Normalize();
            var dfDot = lightDir.Dot(normal);
            MathHelper.Saturate(ref dfDot);
            var diffuse = ColorDiffuse.ToVector() * dfDot * Strength;

            // Specular highlight
            var Reflection = 2 * dfDot * normal - lightDir;
            Reflection.Normalize();
            var view = Camera.Position - point;
            view.Normalize();
            var spDot = Reflection.Dot(view);
            MathHelper.Saturate(ref spDot);
            var specular = ColorSpecular.ToVector() * spDot * Strength;

            // Compute self shadowing
            var shadow = 4.0f * lightDir.Dot(normal);
            MathHelper.Saturate(ref shadow);


            // Final result
            var colorVector = ambient + shadow * (diffuse + specular);
            //var colorVector = ambient + diffuse;

			return colorVector.ToColorWithClamp();
		}
    }
}
