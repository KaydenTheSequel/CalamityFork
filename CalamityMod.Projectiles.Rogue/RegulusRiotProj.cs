using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class RegulusRiotProj : ModProjectile, ILocalizedModType, IModType
{
	private bool canHome;

	private int homingDelay;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/RegulusRiot";

	public ref float Timer => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 28;
		base.Projectile.height = 28;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 3;
		base.Projectile.extraUpdates = 1;
		base.Projectile.alpha = 255;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.timeLeft = 240;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = base.Projectile.MaxUpdates * 15;
	}

	public override void AI()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 20;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		Dust dust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralBlue>(), 0f, 0f, 100);
		dust.noGravity = true;
		dust.velocity = Vector2.Zero;
		Dust dust2 = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100);
		dust2.noGravity = true;
		dust2.velocity = Vector2.Zero;
		Timer++;
		base.Projectile.rotation -= (float)Math.PI / 30f;
		if (homingDelay > 0)
		{
			homingDelay--;
		}
		if (Timer > 30f && homingDelay == 0)
		{
			canHome = true;
		}
		if (Timer == 30f && !canHome)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.5f;
			canHome = true;
		}
		if (!canHome)
		{
			return;
		}
		Vector2 targetCenter = base.Projectile.Center;
		float homingRange = 500f;
		int targetIndex = -1;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			float extraDistance = n.width / 2 + n.height / 2;
			if (n.CanBeChasedBy(base.Projectile) && base.Projectile.WithinRange(n.Center, homingRange + extraDistance))
			{
				float currentNPCDist = Vector2.Distance(n.Center, base.Projectile.Center);
				if (currentNPCDist < homingRange)
				{
					homingRange = currentNPCDist;
					targetIndex = n.whoAmI;
				}
			}
		}
		if (targetIndex != -1)
		{
			targetCenter = Main.npc[targetIndex].Center;
			base.Projectile.velocity = (base.Projectile.velocity * 15f + Vector2.Normalize(targetCenter - base.Projectile.Center) * 14f) / 16f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);
		if (Timer > 30f)
		{
			canHome = false;
			homingDelay = 20;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.position);
		for (int i = 0; i < 10; i++)
		{
			Dust dust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralBlue>(), 0f, 0f, 100, default(Color), 1.5f);
			dust.noGravity = true;
			dust.velocity = Vector2.Zero;
			Dust dust2 = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 1.5f);
			dust2.noGravity = true;
			dust2.velocity = Vector2.Zero;
		}
		if (base.Projectile.Calamity().stealthStrike && base.Projectile.owner == Main.myPlayer)
		{
			for (int j = 0; j < 5; j++)
			{
				Vector2 velocity = ((float)Math.PI * 2f * (float)j / 5f - ((float)Math.PI / 3f - base.Projectile.velocity.ToRotation())).ToRotationVector2() * 2f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<RegulusEnergy>(), (int)((float)base.Projectile.damage * 0.45f), base.Projectile.knockBack, base.Projectile.owner);
			}
		}
	}
}
