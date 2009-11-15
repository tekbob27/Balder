using Balder.Core.Display;
using Balder.Core.Extensions;
using Balder.Core.Math;

namespace Balder.Core.Lighting
{
	public class OmniLight : Light
	{
		public float Strength;
        public float Range;

		public OmniLight()
		{
			Strength = 1f;
            Range = 10.0f;
		}

		public override Color Calculate(Viewport viewport, Vector point, Vector normal, Color vectorAmbient, Color vectorDiffuse, Color vectorSpecular)
		{
			var actualAmbient = Ambient + vectorAmbient;
			var actualDiffuse = Diffuse + vectorDiffuse;
			var actualSpecular = Specular + vectorSpecular;

            // Use dotproduct for diffuse lighting. Add point functionality as this now is a directional light.
            // Ambient light
            var ambient = actualAmbient  * Strength;

            // Diffuse light
            var lightDir = Position - point;
            lightDir.Normalize();
            normal.Normalize();
            var dfDot = lightDir.Dot(normal);
            MathHelper.Saturate(ref dfDot);
            var diffuse = actualDiffuse * dfDot * Strength;

            // Specular highlight
            var reflection = 2f * dfDot * normal - lightDir;
            reflection.Normalize();
            var view = viewport.Camera.Position - point;
            view.Normalize();
            var spDot = reflection.Dot(view);
            MathHelper.Saturate(ref spDot);
            var specular = actualSpecular * spDot * Strength;

            // Compute self shadowing
            var shadow = 4.0f * lightDir.Dot(normal);
            MathHelper.Saturate(ref shadow);

            // Compute range for the light
            var attenuation = ((lightDir / Range).Dot(lightDir / Range));
            MathHelper.Saturate(ref attenuation);
            attenuation = 1f - attenuation;

            // Final result
            var colorVector = ambient + shadow * (diffuse + specular) * attenuation;
            //var colorVector = ambient + diffuse;

			return colorVector;
		}

	}
}
