using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class ReboundingRainbowProj : ModProjectile, ILocalizedModType, IModType
{
	private int Lifetime = 400;

	private int ReboundTime = 30;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/ReboundingRainbow";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 56;
		base.Projectile.height = 56;
		base.Projectile.ignoreWater = true;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 6;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		SpawnProjectilesNearEnemies();
		BoomerangAI();
		LightingAndDust();
	}

	private void BoomerangAI()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += 0.4f * (float)base.Projectile.direction;
		if (base.Projectile.soundDelay == 0)
		{
			base.Projectile.soundDelay = 8;
			SoundEngine.PlaySound(in SoundID.Item7, base.Projectile.position);
		}
		int timeMult = ((!base.Projectile.Calamity().stealthStrike) ? 1 : ReboundingRainbow.stealthTimeMult);
		if (base.Projectile.timeLeft < Lifetime * timeMult - ReboundTime * timeMult)
		{
			base.Projectile.ai[0] = 1f;
		}
		if (base.Projectile.ai[0] != 1f)
		{
			return;
		}
		Player player = Main.player[base.Projectile.owner];
		float returnSpeed = 14f;
		float acceleration = (base.Projectile.Calamity().stealthStrike ? 0.45f : 0.6f);
		Vector2 playerVec = player.Center - base.Projectile.Center;
		if (((Vector2)(ref playerVec)).Length() > 3000f)
		{
			base.Projectile.Kill();
		}
		((Vector2)(ref playerVec)).Normalize();
		playerVec *= returnSpeed;
		if (base.Projectile.velocity.X < playerVec.X)
		{
			base.Projectile.velocity.X += acceleration;
			if (base.Projectile.velocity.X < 0f && playerVec.X > 0f)
			{
				base.Projectile.velocity.X += acceleration;
			}
		}
		else if (base.Projectile.velocity.X > playerVec.X)
		{
			base.Projectile.velocity.X -= acceleration;
			if (base.Projectile.velocity.X > 0f && playerVec.X < 0f)
			{
				base.Projectile.velocity.X -= acceleration;
			}
		}
		if (base.Projectile.velocity.Y < playerVec.Y)
		{
			base.Projectile.velocity.Y += acceleration;
			if (base.Projectile.velocity.Y < 0f && playerVec.Y > 0f)
			{
				base.Projectile.velocity.Y += acceleration;
			}
		}
		else if (base.Projectile.velocity.Y > playerVec.Y)
		{
			base.Projectile.velocity.Y -= acceleration;
			if (base.Projectile.velocity.Y > 0f && playerVec.Y < 0f)
			{
				base.Projectile.velocity.Y -= acceleration;
			}
		}
		if (Main.myPlayer == base.Projectile.owner)
		{
			Rectangle hitbox = base.Projectile.Hitbox;
			if (((Rectangle)(ref hitbox)).Intersects(player.Hitbox))
			{
				base.Projectile.Kill();
			}
		}
	}

	private void SpawnProjectilesNearEnemies()
	{
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		if (!base.Projectile.friendly)
		{
			return;
		}
		float maxDistance = 300f;
		bool homeIn = false;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			if (npc.CanBeChasedBy(base.Projectile))
			{
				float extraDistance = npc.width / 2 + npc.height / 2;
				bool canHit = true;
				if (extraDistance < maxDistance)
				{
					canHit = Collision.CanHit(base.Projectile.Center, 1, 1, npc.Center, 1, 1);
				}
				if ((Vector2.Distance(npc.Center, base.Projectile.Center) < maxDistance + extraDistance) & canHit)
				{
					homeIn = true;
					break;
				}
			}
		}
		if (!homeIn)
		{
			return;
		}
		int counter = (base.Projectile.Calamity().stealthStrike ? 40 : 60);
		if (Main.player[base.Projectile.owner].miscCounter % counter != 0)
		{
			return;
		}
		int splitProj = ModContent.ProjectileType<ReboundingRainbowSplit>();
		if (base.Projectile.owner != Main.myPlayer || Main.player[base.Projectile.owner].ownedProjectileCounts[splitProj] >= 16)
		{
			return;
		}
		for (int i = 0; i < 8; i++)
		{
			Vector2 velocity = ((float)Math.PI * 2f * (float)i / 8f - (MathHelper.ToRadians(67.5f) - base.Projectile.velocity.ToRotation())).ToRotationVector2() * 4f;
			Projectile split = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, splitProj, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			if (base.Projectile.Calamity().stealthStrike)
			{
				split.idStaticNPCHitCooldown = 8;
				split.usesIDStaticNPCImmunity = true;
				split.timeLeft = 60;
			}
		}
	}

	private void LightingAndDust()
	{
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(3))
		{
			int rainbow = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 66, base.Projectile.direction * 2, 0f, 150, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.3f);
			Main.dust[rainbow].noGravity = true;
			Dust obj = Main.dust[rainbow];
			obj.velocity *= 0f;
		}
		Lighting.AddLight(base.Projectile.Center, 0.15f, 1f, 0.25f);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<ElementalMix>(), 60);
		if (!base.Projectile.Calamity().stealthStrike)
		{
			base.Projectile.ai[0] = 1f;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (!base.Projectile.Calamity().stealthStrike)
		{
			base.Projectile.ai[0] = 1f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}
}
