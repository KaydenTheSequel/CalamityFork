using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Items.Dyes;

public abstract class BaseDye : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Dyes";

	public abstract ArmorShaderData ShaderDataToBind { get; }

	public sealed override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			GameShaders.Armor.BindShader(base.Item.type, ShaderDataToBind);
		}
		SafeSetStaticDefaults();
	}

	public sealed override void SetDefaults()
	{
		int dye = base.Item.dye;
		base.Item.CloneDefaults(3561);
		base.Item.dye = dye;
		SafeSetDefaults();
	}

	public virtual void SafeSetDefaults()
	{
	}

	public virtual void SafeSetStaticDefaults()
	{
	}
}
