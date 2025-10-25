using System.Reflection;
using UnityEngine;

namespace CopyCode.Helpers;

internal static class Asset
{
	/// <summary>
	///     Loads the given image and creates a texture from it
	/// </summary>
	public static Texture2D? GetTexture(string name) {
		Stream? stream = Assembly.GetAssembly(typeof(Plugin)).GetManifestResourceStream(name);

		if (stream == null) return null;

		using var memoryStream = new MemoryStream();
		stream.CopyTo(memoryStream);

		byte[] bytes = memoryStream.ToArray();

		if (bytes.Length == 0) return null;

		var t = new Texture2D(1, 1);

		t.LoadImage(bytes);

		return t;
	}
}