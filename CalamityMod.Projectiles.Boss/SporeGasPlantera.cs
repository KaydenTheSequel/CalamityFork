using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class SporeGasPlantera : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "Terraria/Images/Projectile_" + (short)569;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 2;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] > (Main.getGoodWorld ? 600f : 900f))
		{
			base.Projectile.localAI[0] += 10f;
			base.Projectile.damage = 0;
		}
		if (base.Projectile.localAI[0] > 255f)
		{
			base.Projectile.Kill();
			base.Projectile.localAI[0] = 255f;
		}
		float lightValues = (float)(255 - base.Projectile.alpha) * 0.6f / 255f;
		Lighting.AddLight(base.Projectile.Center, 0f, lightValues, 0f);
		base.Projectile.alpha = (int)(100.0 + (double)base.Projectile.localAI[0] * 0.7);
		base.Projectile.rotation += base.Projectile.velocity.X * 0.02f;
		base.Projectile.rotation += (float)base.Projectile.direction * 0.002f;
		if (((Vector2)(ref base.Projectile.velocity)).Length() > (Main.getGoodWorld ? 4f : 2f))
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.985f;
		}
	}

	public override bool CanHitPlayer(Player target)
	{
		if (base.Projectile.ai[1] <= (Main.getGoodWorld ? 600f : 900f))
		{
			return base.Projectile.ai[1] > 120f;
		}
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] > (Main.getGoodWorld ? 600f : 900f))
		{
			byte b2 = (byte)((26f - (base.Projectile.ai[1] - (Main.getGoodWorld ? 600f : 900f))) * 10f);
			byte a2 = (byte)((float)base.Projectile.alpha * ((float)(int)b2 / 255f));
			return new Color((int)b2, (int)b2, (int)b2, (int)a2);
		}
		return new Color(255, 255, 255, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		switch ((int)base.Projectile.ai[0])
		{
		case 1:
			Main.instance.LoadProjectile(570);
			texture = TextureAssets.Projectile[570].Value;
			break;
		case 2:
			Main.instance.LoadProjectile(571);
			texture = TextureAssets.Projectile[571].Value;
			break;
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 1, texture);
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0 && base.Projectile.ai[1] <= (Main.getGoodWorld ? 600f : 900f) && base.Projectile.ai[1] > 120f)
		{
			target.AddBuff(20, 480);
		}
	}
}
