using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Graphics.Primitives;

public sealed class PrimitiveRenderSystem : ModSystem
{
	public override void Load()
	{
		if (!Main.dedServ)
		{
			Main.QueueMainThreadAction(delegate
			{
				SanePrimitiveRenderer.Initialize();
			});
		}
	}

	public override void Unload()
	{
		SanePrimitiveRenderer.Dispose();
	}
}
