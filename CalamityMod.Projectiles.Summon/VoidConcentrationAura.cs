using System.IO;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class VoidConcentrationAura : ModProjectile, ILocalizedModType, IModType
{
	public int timer;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 46;
		base.Projectile.height = 80;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 3f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(timer);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		timer = reader.ReadInt32();
	}

	public override bool? CanCutTiles()
	{
		return true;
	}

	public void HandleRightClick()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		Vector2 velocity = Main.MouseWorld - Main.player[base.Projectile.owner].Center;
		((Vector2)(ref velocity)).Normalize();
		velocity *= 2f;
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Main.player[base.Projectile.owner].Center, velocity, ModContent.ProjectileType<VoidConcentrationBlackhole>(), (int)((float)base.Projectile.damage * 5f), 0f, base.Projectile.owner);
		base.Projectile.Kill();
	}

	public override void AI()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		Player owner = Main.player[base.Projectile.owner];
		CalamityPlayer mp = owner.Calamity();
		base.Projectile.Center = owner.Center;
		mp.voidAuraDamage = true;
		if (owner.dead)
		{
			mp.voidAuraDamage = false;
		}
		if (!mp.voidAuraDamage || (!mp.voidConcentrationAura && base.Projectile.ai[0] == 1f))
		{
			mp.voidAura = false;
			base.Projectile.Kill();
		}
		if (owner.whoAmI == Main.myPlayer && owner.ownedProjectileCounts[base.Type] <= 25 && timer > 0 && timer % 4 == 0)
		{
			NPC target = base.Projectile.Center.MinionHoming(1800f, owner);
			if (target != null)
			{
				Vector2 correctedVelocity = target.Center - owner.Center;
				((Vector2)(ref correctedVelocity)).Normalize();
				correctedVelocity *= 3f;
				int perturbificator9000 = Main.rand.Next(-1, 2);
				Vector2 perturbedspeed = Utils.RotatedBy(new Vector2(correctedVelocity.X + (float)perturbificator9000, correctedVelocity.Y + (float)perturbificator9000), (double)MathHelper.ToRadians((float)Main.rand.Next(1, 3)), default(Vector2));
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, perturbedspeed, ModContent.ProjectileType<VoidConcentrationOrb>(), (int)((float)base.Projectile.damage * 0.75f), 0f, owner.whoAmI);
			}
			timer = -1;
		}
		base.Projectile.ai[0] = 1f;
		if (timer <= 50 || timer % 4 != 0)
		{
			timer++;
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}
}
