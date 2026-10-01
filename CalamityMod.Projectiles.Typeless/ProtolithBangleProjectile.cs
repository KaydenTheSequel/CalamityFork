using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class ProtolithBangleProjectile : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public int damageTime = 24;

	public int Soundtime1 = 2;

	public int Soundtime2 = 8;

	public int Soundtime3 = 15;

	public int explosionSize = 230;

	public SlotId SoundSlot;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public bool visual => Owner.Calamity().protolithBangleVisual;

	public override void SetDefaults()
	{
		base.Projectile.width = explosionSize;
		base.Projectile.height = explosionSize;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 0;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = damageTime + 20;
		base.Projectile.ArmorPenetration = 25;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.scale = 0.8f;
	}

	public override void AI()
	{
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		if (!(base.Projectile.ai[0] < 0f) && !(base.Projectile.ai[0] > 199f) && time <= damageTime && Main.npc[(int)base.Projectile.ai[0]].active && Main.npc[(int)base.Projectile.ai[0]].life > 0)
		{
			base.Projectile.Center = Main.npc[(int)base.Projectile.ai[0]].Center;
		}
		Vector2 particlePlace = Vector2.UnitY * 40f;
		if (time == Soundtime1)
		{
			MakePusle(base.Projectile.Center + particlePlace);
		}
		if (time == Soundtime2)
		{
			MakePusle(base.Projectile.Center + particlePlace.RotatedBy(2.094395160675049));
		}
		if (time == Soundtime3)
		{
			MakePusle(base.Projectile.Center + particlePlace.RotatedBy(-2.094395160675049));
		}
		if (time == damageTime)
		{
			float visMult = (visual ? 1f : 0.3f);
			base.Projectile.ai[0] = -1f;
			if (visual)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Vector2.Zero, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 15, 1.2f, Color.Gold, base.Projectile.scale * new Vector2(1f, 1.3f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.9f));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/BloomRing", base.Projectile.scale * new Vector2(1.4f, 0.6f), 0f, 0.3f, 1.35f, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			for (int i = 0; i < 28; i++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, (Vector2.One * Main.rand.NextFloat(8f, 13f)).RotatedByRandom(6.2831854820251465), "CalamityMod/Particles/Square", affectedByGravity: true, Main.rand.Next(40, 71), base.Projectile.scale * Main.rand.NextFloat(0.08f, 0.14f) * 15f, Color.Lerp(Color.White, Color.Khaki, Main.rand.NextFloat()) * visMult, new Vector2(1f, Main.rand.NextFloat(1f, 2f)), useAddativeBlend: false, glowCenter: false, Main.rand.NextFloat(-4f, 4f)));
			}
		}
		if (time == 0 && visual)
		{
			SoundStyle sound = new SoundStyle("CalamityMod/Sounds/Item/ProtolithBangleSound");
			SoundStyle style = sound with
			{
				Volume = 1f,
				MaxInstances = -1
			};
			SoundSlot = SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		if (SoundEngine.TryGetActiveSound(SoundSlot, out ActiveSound Sound) && Sound.IsPlaying)
		{
			Sound.Position = base.Projectile.Center;
		}
		time++;
	}

	public void MakePusle(Vector2 position)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		if (visual)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(position, Vector2.Zero, Color.White, "CalamityMod/Particles/BloomRing", base.Projectile.scale * Vector2.One, 0f, 0.3f, 0.65f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			for (int i = 0; i < 18; i++)
			{
				Vector2 outerVel = Vector2.One.RotatedByRandom(6.2831854820251465) * Main.rand.NextFloat(5f, 7f);
				GeneralParticleHandler.SpawnParticle(new CustomSpark(position + outerVel * 5f, -outerVel * 0.6f, "CalamityMod/Particles/Square", affectedByGravity: false, Main.rand.Next(10, 16), base.Projectile.scale * Main.rand.NextFloat(0.08f, 0.14f) * 10f, Color.Lerp(Color.White, Color.Khaki, Main.rand.NextFloat()), new Vector2(1f, Main.rand.NextFloat(1f, 2f)), useAddativeBlend: false, glowCenter: false, Main.rand.NextFloat(-4f, 4f)));
			}
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		modifiers.SetCrit();
		float critDamage = Math.Min(Owner.GetTotalCritChance(AverageDamageClass.Instance) * 0.01f, 1f);
		float minMult = 0.1f;
		int hitsToMinMult = 5;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult + critDamage;
		Vector2 launchVel = base.Projectile.Center.DirectionTo(target.Center) - Vector2.UnitY;
		float launchPower = 6f;
		target.MoveNPC(launchVel, launchPower, ignoreKBImmune: true);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, (float)base.Projectile.width * 0.5f * base.Projectile.scale, targetHitbox);
	}

	public override bool? CanDamage()
	{
		if (time >= damageTime)
		{
			return null;
		}
		return false;
	}
}
