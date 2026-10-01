using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class EyeOfNightSummon : ModProjectile, ILocalizedModType, IModType
{
	public const int ShootRate = 60;

	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public NPC Target
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return Owner.Center.MinionHoming(750f, Owner, CalamityPlayer.areThereAnyDamnBosses);
		}
	}

	public ref float HoverTime => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 16);
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 90000;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		ProvidePlayerMinionBuffs();
		GenerateVisuals();
		if (Target == null)
		{
			FlyNearOwner();
		}
		else
		{
			AttackTarget(Target);
		}
	}

	internal void ProvidePlayerMinionBuffs()
	{
		Owner.AddBuff(ModContent.BuffType<EyeOfNightBuff>(), 3600);
		if (base.Projectile.type == ModContent.ProjectileType<EyeOfNightSummon>())
		{
			if (Owner.dead)
			{
				Owner.Calamity().eyeOfNight = false;
			}
			if (Owner.Calamity().eyeOfNight)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	internal void GenerateVisuals()
	{
		if (!Main.dedServ)
		{
			base.Projectile.rotation += base.Projectile.velocity.X * 0.075f;
		}
	}

	internal void FlyNearOwner()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		Vector2 destination = Owner.Top - Vector2.UnitY * 45f + ((float)base.Projectile.identity * 0.9f).ToRotationVector2() * 16f;
		Vector2 idealVelocity = base.Projectile.SafeDirectionTo(destination) * MathHelper.Lerp(2.3f, 8f, Utils.GetLerpValue(16f, 160f, base.Projectile.Distance(destination)));
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 0.4f)
		{
			base.Projectile.velocity = Vector2.UnitY.RotatedBy(Main.rand.NextFloat(0.5f, 1.1f) * (float)Main.rand.NextBool().ToDirectionInt()) * -3.6f;
		}
		else if (!base.Projectile.WithinRange(destination, 20f))
		{
			base.Projectile.velocity = base.Projectile.velocity * 0.9f + idealVelocity * 0.1f;
		}
		if (!base.Projectile.WithinRange(Owner.Center, 1800f))
		{
			base.Projectile.Center = Owner.Center;
			base.Projectile.velocity = -Vector2.UnitY * 4f;
			base.Projectile.netUpdate = true;
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.985f;
	}

	internal void AttackTarget(NPC target)
	{
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner && HoverTime % 70f == 69f)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.SafeDirectionTo(target.Center) * 8f, ModContent.ProjectileType<EyeOfNightCell>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			HoverTime++;
		}
		Vector2 destination = target.Center;
		Vector2 destinationOffset = Vector2.Max(target.Size * 1.2f, Vector2.One * 90f).RotatedBy((float)base.Projectile.identity * 0.96f + HoverTime / 15f);
		destinationOffset *= MathHelper.Lerp(0.7f, 1.3f, (float)Math.Cos((float)base.Projectile.identity * 1.11f + HoverTime / 14f) * 0.5f + 0.5f);
		destinationOffset.Y += (float)Math.Sin((float)base.Projectile.identity * 1.16f + HoverTime / 15f + (float)Math.PI / 2f) * MathHelper.Min((float)target.height * 0.8f, 70f);
		destination += destinationOffset;
		float flySpeed = MathHelper.Lerp(5f, 15f, Utils.GetLerpValue(40f, 250f, base.Projectile.Distance(destination), clamped: true));
		if (base.Projectile.WithinRange(destination, 24f + ((Vector2)(ref target.velocity)).Length() * 2f))
		{
			HoverTime++;
		}
		base.Projectile.velocity = (destination - base.Projectile.Center).SafeNormalize(Vector2.Zero) * flySpeed;
	}
}
