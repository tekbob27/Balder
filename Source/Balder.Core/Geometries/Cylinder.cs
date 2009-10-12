namespace Balder.Core.Geometries
{
	public class Cylinder : Geometry
	{
		public Cylinder(float radius, float height, int segments, int heightSegments)
		{
            /*
                 Ok. Da er det lagd..      En liten forklaring: Cylinder er nå en klasse som ligger i Geometries - den arver fra Geometry. 
             * I Geometry er det en injected property som heter GeometryContext - den bruker du for å lage vertices og faces
             * (husk å bruke allocate... metodene først).  I interface som heter IContentManager har jeg nå lagt til en metode som
             * heter CreateCylinder() - denne er også da implementert i implementasjonen og bruker NInject sin kernel for å lage Cylinder
             * - grunnen til det er at GeometryContext er en injected property.
                 Hvis du vil endre på argumentene som går inn på constructoren, så må du være nøye med navgivningen og gjøre 
             * samme endringen i ContentManager sin CreateCylinder som setter opp argumentene som strings
                 Jeg ser at jeg har lyst til å lage en Extension metode for å gjøre dette enklere - men det gjør jeg seinere.
            */

            //GeometryContext.AllocateVertices(10);
            //for(int i = 0; i < 10; i++)
            //{
            //    GeometryContext.SetVertex(i,new Vertex(i,i,i));
            //}

			// + faces.. :)
		}
	}
}
