#if(SILVERLIGHT)
using System.Windows.Media;
#else
using System.Drawing;
#endif
using Balder.Core.Display;
using Balder.Core.Extensions;
using Balder.Core.Math;

namespace Balder.Core.Lighting
{
	public class OmniLight : Light
	{
		public float Strength;
        public float Range;
        public bool Specular;
        public bool Diffuse;
        public bool Ambient;

		public OmniLight()
		{
			Strength = 1f;
            Range = 10.0f;
            Specular = true;
            Diffuse = true;
            Ambient = true;
		}

		public override Color Calculate(Viewport viewport, Vector point, Vector normal)
		{
            // Use dotproduct for diffuse lighting. Add point functionality as this now is a directional light.
            // Ambient light
            var ambient = ColorAmbient.ToVector() * Strength;

            // Diffuse light
            var lightDir = Position - point;
            lightDir.Normalize();
            normal.Normalize();
            var dfDot = lightDir.Dot(normal);
            MathHelper.Saturate(ref dfDot);
            var diffuse = ColorDiffuse.ToVector() * dfDot * Strength;

            // Specular highlight
            var Reflection = 2 * dfDot * normal - lightDir;
            Reflection.Normalize();
            var view = viewport.Camera.Position - point;
            view.Normalize();
            var spDot = Reflection.Dot(view);
            MathHelper.Saturate(ref spDot);
            var specular = ColorSpecular.ToVector() * spDot * Strength;

            // Compute self shadowing
            var shadow = 4.0f * lightDir.Dot(normal);
            MathHelper.Saturate(ref shadow);

            // Compute range for the light
            var attenuation = ((lightDir / Range).Dot(lightDir / Range));
            MathHelper.Saturate(ref attenuation);
            attenuation = 1 - attenuation;

            // Final result
            var colorVector = ambient + shadow * (diffuse + specular) * attenuation;
            //var colorVector = ambient + diffuse;

			return colorVector.ToColorWithClamp();
		}

	}
}
