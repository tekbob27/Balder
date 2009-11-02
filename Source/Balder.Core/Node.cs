using Balder.Core.Interfaces;
using Balder.Core.Math;

namespace Balder.Core
{
	/// <summary>
	/// Abstract class representing a node in a scene
	/// </summary>
	public abstract class Node
	{
		#region Constructor(s)
		protected Node()
		{
			World = Matrix.Identity;
			Scale = new Vector(1f,1f,1f);
		}
		#endregion

		#region Public Properties

		/// <summary>
		/// Get and set the position in space for the node
		/// </summary>
		public Vector Position;

		/// <summary>
		/// Get and set the scale of the node
		/// </summary>
		public Vector Scale;

		/// <summary>
		/// Get and set the matrix representing the node in the world
		/// </summary>
		public Matrix World;

		/// <summary>
		/// Get and set the name of the node
		/// </summary>
		public string Name;

		/// <summary>
		/// The bounding sphere surrounding the node
		/// </summary>
		public BoundingSphere BoundingSphere;

		/// <summary>
		/// Get and set wether or not the node is visible
		/// </summary>
		public bool IsVisible { get; set; }

		public Scene Scene { get; set; }
		#endregion


		public virtual void Prepare(IViewport viewport) {}
		public virtual void Update() {}

	}
}
