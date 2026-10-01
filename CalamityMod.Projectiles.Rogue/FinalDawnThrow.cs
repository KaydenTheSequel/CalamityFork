using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Rogue;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class FinalDawnThrow : ModProjectile, ILocalizedModType, IModType
{
	public const float DesiredSpeed = 38f;

	public const float InterpolationTime = 15f;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 80;
		base.Projectile.height = 80;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.light = 0f;
		base.Projectile.extraUpdates = 2;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (player == null || player.dead)
		{
			base.Projectile.Kill();
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			SoundEngine.PlaySound(in TheFinalDawn.UseSound, base.Projectile.Center);
			base.Projectile.localAI[0] = 1f;
		}
		base.Projectile.spriteDirection = (base.Projectile.velocity.X > 0f).ToDirectionInt();
		base.Projectile.rotation += 0.25f * (float)base.Projectile.direction;
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= 30f)
		{
			Vector2 desiredVelocity = base.Projectile.SafeDirectionTo(player.Center) * 38f;
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, desiredVelocity, 1f / 15f);
			if (base.Projectile.Distance(player.Center) < 64f)
			{
				base.Projectile.Kill();
			}
		}
		int idx = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<FinalFlame>(), 0f, 0f, 0, default(Color), 0.5f);
		Dust obj = Main.dust[idx];
		obj.velocity *= 0.5f;
		Dust obj2 = Main.dust[idx];
		obj2.velocity += base.Projectile.velocity * 0.5f;
		Main.dust[idx].noGravity = true;
		Main.dust[idx].noLight = false;
		Main.dust[idx].scale = 1f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		Texture2D scytheTexture = TextureAssets.Projectile[base.Type].Value;
		Texture2D scytheGlowTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/FinalDawnThrow_Glow", (AssetRequestMode)2).Value;
		int height = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int yStart = height * base.Projectile.frame;
		Main.spriteBatch.Draw(scytheTexture, base.Projectile.Center - Main.screenPosition + Vector2.UnitY * base.Projectile.gfxOffY, (Rectangle?)new Rectangle(0, yStart, scytheTexture.Width, height), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)scytheTexture.Width / 2f, (float)height / 2f), base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection != 1), 0f);
		Main.spriteBatch.Draw(scytheGlowTexture, base.Projectile.Center - Main.screenPosition + Vector2.UnitY * base.Projectile.gfxOffY, (Rectangle?)new Rectangle(0, yStart, scytheTexture.Width, height), base.Projectile.GetAlpha(Color.White), base.Projectile.rotation, new Vector2((float)scytheTexture.Width / 2f, (float)height / 2f), base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection != 1), 0f);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Dragonfire>(), 240);
	}
}
