using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class FinalDawnFireball : ModProjectile, ILocalizedModType, IModType
{
	public const float DesiredSpeed = 30f;

	public const float InterpolationTime = 10f;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 36;
		base.Projectile.height = 80;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.penetrate = 1;
		base.Projectile.hostile = false;
		base.Projectile.friendly = false;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 50;
		base.Projectile.timeLeft = 180;
	}

	public override void AI()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.npc[(int)base.Projectile.ai[1]].active)
		{
			base.Projectile.Kill();
		}
		int idx = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<FinalFlame>());
		Main.dust[idx].velocity = base.Projectile.velocity * 0.5f;
		Main.dust[idx].noGravity = true;
		Main.dust[idx].noLight = true;
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= 20f)
		{
			base.Projectile.friendly = true;
			NPC npc = Main.npc[(int)base.Projectile.ai[1]];
			Vector2 desiredVelocity = base.Projectile.SafeDirectionTo(npc.Center) * 30f;
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, desiredVelocity, 0.1f);
			if (!npc.active)
			{
				base.Projectile.Kill();
			}
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 5)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
			if (base.Projectile.frame >= Main.projFrames[base.Type])
			{
				base.Projectile.frame = 0;
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<FinalDawnReticle>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		Texture2D glowmask = TextureAssets.Projectile[base.Type].Value;
		int height = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int yStart = height * base.Projectile.frame;
		Main.spriteBatch.Draw(glowmask, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, yStart, glowmask.Width, height), base.Projectile.GetAlpha(Color.White), base.Projectile.rotation, new Vector2((float)glowmask.Width / 2f, (float)height / 2f), base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection != 1), 0f);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Dragonfire>(), 240);
	}
}
