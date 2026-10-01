using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class HypothermiaShard : ModProjectile, ILocalizedModType, IModType
{
	private int counter;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 6;
		base.Projectile.scale = 1f;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.extraUpdates = 3;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.1f, 0f, 0.5f);
		base.Projectile.rotation += base.Projectile.velocity.X * 0.2f;
		if (Main.rand.NextBool(10))
		{
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 318, 0f, 0f, 0, default(Color), 0.8f);
			Main.dust[dust].noGravity = true;
			Dust obj = Main.dust[dust];
			obj.velocity *= 0.2f;
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.996f;
		counter++;
		if (counter > 400)
		{
			base.Projectile.scale -= 0.05f;
			if (base.Projectile.scale <= 0.2f)
			{
				base.Projectile.scale = 0.2f;
				base.Projectile.Kill();
			}
			base.Projectile.width = (int)(6f * base.Projectile.scale);
			base.Projectile.height = (int)(6f * base.Projectile.scale);
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = 0f - oldVelocity.X;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = 0f - oldVelocity.Y;
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Voidfrost>(), 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Voidfrost>(), 120);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 318, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f, 0, default(Color), 0.8f);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		float num = base.Projectile.ai[0];
		if (num != 0f)
		{
			if (num != 1f)
			{
				if (num != 2f)
				{
					if (num == 3f)
					{
						texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/HypothermiaShard4", (AssetRequestMode)2).Value;
					}
				}
				else
				{
					texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/HypothermiaShard3", (AssetRequestMode)2).Value;
				}
			}
			else
			{
				texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/HypothermiaShard2", (AssetRequestMode)2).Value;
			}
		}
		else
		{
			texture = TextureAssets.Projectile[base.Type].Value;
		}
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, texture.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
