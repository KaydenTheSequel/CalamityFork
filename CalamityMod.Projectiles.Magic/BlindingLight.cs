using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class BlindingLight : ModProjectile, ILocalizedModType, IModType
{
	private const float Radius = 1400f;

	private const int Lifetime = 45;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 2;
		base.Projectile.height = 2;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.timeLeft = 45;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 1400f, targetHitbox);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SetCrit();
	}

	public override void AI()
	{
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft == 45)
		{
			ConsumeNearbyBlades();
			DivideDamageAmongstTargets();
		}
		base.Projectile.ai[0]++;
		float progress = (float)Math.Sin(base.Projectile.ai[0] / 45f * (float)Math.PI);
		if (base.Projectile.ai[0] > 55f)
		{
			progress = MathHelper.Lerp(progress, 0f, (base.Projectile.ai[0] - 55f) / 5f);
		}
		if (!Main.dedServ && !CalamityClientConfig.Instance.Photosensitivity && base.Projectile.ai[0] > 15f)
		{
			if (!Filters.Scene["CalamityMod:LightBurst"].IsActive())
			{
				Filters.Scene.Activate("CalamityMod:LightBurst", base.Projectile.Center).GetShader().UseTargetPosition(base.Projectile.Center)
					.UseProgress(0f);
			}
			Filters.Scene["CalamityMod:LightBurst"].GetShader().UseProgress(progress);
		}
	}

	public override void OnKill(int timeLeft)
	{
		if (!Main.dedServ)
		{
			Filters.Scene.Deactivate("CalamityMod:LightBurst");
		}
	}

	private void ConsumeNearbyBlades()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		int lightBlade = ModContent.ProjectileType<LightBlade>();
		int extraDamage = 0;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile otherProj = enumerator.Current;
			if (otherProj.owner == base.Projectile.owner && otherProj.type == lightBlade && !(base.Projectile.Distance(otherProj.Center) > 1400f))
			{
				extraDamage += otherProj.damage / 2;
				otherProj.Kill();
			}
		}
		base.Projectile.damage += extraDamage;
	}

	private void DivideDamageAmongstTargets()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		int numTargets = 0;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			if (!npc.friendly && !npc.dontTakeDamage && !npc.immortal && base.Projectile.Colliding(default(Rectangle), npc.Hitbox))
			{
				numTargets++;
			}
		}
		if (numTargets <= 0)
		{
			numTargets = 1;
		}
		base.Projectile.damage = (int)((double)base.Projectile.damage / Math.Sqrt(numTargets));
	}
}
