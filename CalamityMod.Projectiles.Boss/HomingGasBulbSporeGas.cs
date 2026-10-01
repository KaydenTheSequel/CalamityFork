using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class HomingGasBulbSporeGas : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 2;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
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
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] > (Main.getGoodWorld ? 600f : 900f))
		{
			base.Projectile.localAI[0] += 10f;
		}
		if (base.Projectile.localAI[0] > 255f)
		{
			base.Projectile.Kill();
			base.Projectile.localAI[0] = 255f;
		}
		float lightValues = (float)(255 - base.Projectile.alpha) * 0.6f / 255f;
		Lighting.AddLight(base.Projectile.Center, lightValues, 0f, lightValues);
		base.Projectile.rotation += base.Projectile.velocity.X * 0.02f;
		base.Projectile.rotation += (float)base.Projectile.direction * 0.002f;
		if (((Vector2)(ref base.Projectile.velocity)).Length() > (Main.getGoodWorld ? 2f : 0.5f))
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.985f;
		}
		if (base.Projectile.timeLeft <= 40)
		{
			base.Projectile.Opacity = Utils.GetLerpValue(0f, 40f, base.Projectile.timeLeft);
		}
	}

	public override bool CanHitPlayer(Player target)
	{
		return base.Projectile.Opacity > 0.8f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return lightColor;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		switch ((int)base.Projectile.ai[0])
		{
		case 1:
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/HomingGasBulbSporeGas2", (AssetRequestMode)2).Value;
			break;
		case 2:
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/HomingGasBulbSporeGas3", (AssetRequestMode)2).Value;
			break;
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor * base.Projectile.Opacity, 1, texture);
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0 && base.Projectile.ai[1] <= (Main.getGoodWorld ? 600f : 900f) && base.Projectile.ai[1] > 120f)
		{
			target.AddBuff(20, 240);
		}
	}
}
