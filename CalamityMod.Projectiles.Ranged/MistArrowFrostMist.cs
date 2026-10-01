using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class MistArrowFrostMist : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 24);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 60;
		base.Projectile.alpha = 96;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.coldDamage = true;
	}

	public override void AI()
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft < 20)
		{
			base.Projectile.alpha += 8;
			if (base.Projectile.alpha >= 255)
			{
				base.Projectile.alpha = 255;
				base.Projectile.Kill();
			}
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.975f;
		for (int i = 0; i < 2; i++)
		{
			Dust.NewDustPerfect(new Vector2(base.Projectile.position.X + Main.rand.NextFloat(0f, base.Projectile.width), base.Projectile.position.Y + Main.rand.NextFloat(0f, base.Projectile.height)), 16, Vector2.Zero, 0, default(Color), 0.45f).noGravity = true;
		}
		Lighting.AddLight(base.Projectile.Center, 0.4f, 0.4f, 0.4f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		float num = base.Projectile.ai[0];
		if (num != 0f)
		{
			if (num != 1f)
			{
				if (num == 2f)
				{
					texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/MistArrowFrostMist3", (AssetRequestMode)2).Value;
				}
			}
			else
			{
				texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/MistArrowFrostMist2", (AssetRequestMode)2).Value;
			}
		}
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, texture.Size() / 2f, 1f, (SpriteEffects)0);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(324, 45);
	}
}
