using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class AstralSpray : ModProjectile, ILocalizedModType, IModType
{
	public static int ConversionType;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float Time => ref base.Projectile.ai[0];

	public bool ShotFromTerraformer => base.Projectile.ai[1] == 1f;

	public override void SetStaticDefaults()
	{
		ConversionType = ModContent.GetInstance<AstralConversion>().Type;
	}

	public override void SetDefaults()
	{
		base.Projectile.DefaultToSpray();
		base.Projectile.aiStyle = 0;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void AI()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 133)
		{
			base.Projectile.timeLeft = 133;
		}
		if (Main.myPlayer == base.Projectile.owner)
		{
			int size = (ShotFromTerraformer ? 3 : 2);
			Point tileCenter = base.Projectile.Center.ToTileCoordinates();
			WorldGen.Convert(tileCenter.X, tileCenter.Y, ConversionType, size);
		}
		float dustStart = (ShotFromTerraformer ? 3f : 7f);
		if (Time > dustStart)
		{
			float dustScale = Utils.Remap(Time, dustStart + 1f, dustStart + 5f, 0.2f, 1f);
			int dustArea = 0;
			if (ShotFromTerraformer)
			{
				dustScale *= 1.2f;
				dustArea = (int)(12f * dustScale);
			}
			Dust dust = Dust.NewDustDirect(base.Projectile.position - Vector2.One * (float)dustArea, base.Projectile.width + dustArea * 2, base.Projectile.height + dustArea * 2, 118, base.Projectile.velocity.X * 0.4f, base.Projectile.velocity.Y * 0.4f, 100);
			dust.noGravity = true;
			dust.scale *= 1.75f * dustScale;
		}
		Time++;
		base.Projectile.rotation += 0.3f * (float)base.Projectile.direction;
	}
}
