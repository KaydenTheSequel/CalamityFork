using CalamityMod.Dusts;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class StarStruckWaterBottle : ModProjectile, ILocalizedModType, IModType
{
	public static int ConversionType;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Items/Weapons/Typeless/StarStruckWater";

	public override void SetStaticDefaults()
	{
		ConversionType = ModContent.GetInstance<AstralConversion>().Type;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.aiStyle = 2;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
	}

	public override void AI()
	{
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= 10f)
		{
			base.Projectile.velocity.Y += 0.1f;
			base.Projectile.velocity.X *= 0.998f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			SoundEngine.PlaySound(in SoundID.Shatter, base.Projectile.position);
			for (int index = 0; index < 5; index++)
			{
				Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 13);
			}
			for (int i = 0; i < 30; i++)
			{
				int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralBlue>(), 0f, -2f, 0, default(Color), 1.1f);
				Dust obj = Main.dust[index2];
				obj.alpha = 100;
				obj.velocity.X *= 1.5f;
				obj.velocity *= 3f;
			}
			if (Main.myPlayer == base.Projectile.owner)
			{
				Point tileCenter = base.Projectile.Center.ToTileCoordinates();
				WorldGen.Convert(tileCenter.X, tileCenter.Y, ConversionType);
			}
		}
	}

	public override bool? CanCutTiles()
	{
		return false;
	}
}
