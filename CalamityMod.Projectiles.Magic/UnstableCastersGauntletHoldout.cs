using System.Collections.Generic;
using CalamityMod.CalPlayer;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class UnstableCastersGauntletHoldout : BaseGunHoldoutProjectile
{
	private float currentRecoilRotation;

	private float RecoilRotationAmount = 0.24f;

	private float RotationResolveSpeed = 0.24f;

	public override int AssociatedItemID => ModContent.ItemType<UnstableCastersGauntlet>();

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float shootingTimer => ref base.Projectile.ai[0];

	public float speedModifier => (float)base.HeldItem.useTime / 20f;

	public override void SetDefaults()
	{
		base.Projectile.width = 1;
		base.Projectile.height = 1;
		base.Projectile.friendly = true;
		base.Projectile.hostile = false;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ownerHitCheck = true;
	}

	public override void HoldoutAI()
	{
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0810: Unknown result type (might be due to invalid IL or missing references)
		//IL_081b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0858: Unknown result type (might be due to invalid IL or missing references)
		//IL_085d: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer calPlayer = base.Owner.Calamity();
		base.Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.None, 0f);
		if (base.Owner.ownedProjectileCounts[ModContent.ProjectileType<SigilSet>()] == 1 && base.Owner.altFunctionUse == 0)
		{
			float sigilFireRate = (float)base.HeldItem.useTime * 4f;
			if (shootingTimer >= sigilFireRate)
			{
				List<Projectile> activeSigils = new List<Projectile>();
				for (int i = 0; i < Main.maxProjectiles; i++)
				{
					Projectile proj = Main.projectile[i];
					if (proj.active && proj.owner == base.Projectile.owner && (proj.type == ModContent.ProjectileType<IgnisSigil>() || proj.type == ModContent.ProjectileType<AquaSigil>() || proj.type == ModContent.ProjectileType<TerraSigil>() || proj.type == ModContent.ProjectileType<AerSigil>() || proj.type == ModContent.ProjectileType<OrdoSigil>() || proj.type == ModContent.ProjectileType<PerditoSigil>() || proj.type == ModContent.ProjectileType<WarpSigil>()) && proj.ai[2] <= 0f)
					{
						activeSigils.Add(proj);
					}
				}
				if (activeSigils.Count > 0)
				{
					int randomIndex = Main.rand.Next(activeSigils.Count);
					Projectile projectile = activeSigils[randomIndex];
					projectile.ai[2] = 1f;
					if (projectile.type == ModContent.ProjectileType<WarpSigil>())
					{
						shootingTimer = (float)base.HeldItem.useTime * 1.65f;
					}
					else
					{
						shootingTimer = 0f;
					}
				}
			}
			if (base.Owner.ownedProjectileCounts[ModContent.ProjectileType<SigilSet>()] > 0)
			{
				Projectile sigilParent = null;
				int sigilType = ModContent.ProjectileType<SigilSet>();
				ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Projectile p = enumerator.Current;
					if (p.type == sigilType && p.owner == base.Projectile.owner)
					{
						sigilParent = p;
						break;
					}
				}
				if (sigilParent != null)
				{
					Vector2 randomSpawnOffset = Main.rand.NextVector2Circular(90f, 90f);
					Vector2 dustSpawnPosition = GunTipPosition + randomSpawnOffset;
					Vector2 inwardVelocity = (GunTipPosition - dustSpawnPosition).SafeNormalize(Vector2.UnitY) * Main.rand.NextFloat(7f, 10f);
					Dust dust = Dust.NewDustPerfect(dustSpawnPosition, ModContent.DustType<LightDust>(), inwardVelocity, 0, Color.DarkMagenta, 1.2f);
					dust.noGravity = true;
					dust.velocity *= 0.5f;
					dust.fadeIn = 0.5f;
					dust.scale *= 0.4f;
					Vector2 gunTipPosition = GunTipPosition;
					Color darkMagenta = Color.DarkMagenta;
					Lighting.AddLight(gunTipPosition, 0.4f * ((Color)(ref darkMagenta)).ToVector3());
				}
			}
		}
		else if (base.Owner.altFunctionUse == 0)
		{
			int needleFireRate = (int)(12f * speedModifier);
			if (shootingTimer >= (float)needleFireRate)
			{
				if (calPlayer.unstableCastersGauntletVis >= 0.6f)
				{
					calPlayer.unstableCastersGauntletVis -= 0.6f;
					Projectile projectile2 = base.Projectile;
					projectile2.velocity *= Main.rand.NextFloat(0.97f, 1.04f);
					currentRecoilRotation -= RecoilRotationAmount;
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/UnstableCastersGauntlet/VisNeedleFire");
					style.Volume = 0.4f;
					style.PitchVariance = 0.15f;
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(GunTipPosition, base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 5f, Color.DarkMagenta * 4f, new Vector2(0.4f, 0.8f), base.Projectile.velocity.ToRotation(), 0.07f, 0.3f, 16));
					if (Main.myPlayer == base.Projectile.owner)
					{
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, (base.Projectile.velocity * 35f).RotatedBy(Main.rand.NextFloat(-0.07f, 0.07f)), ModContent.ProjectileType<VisNeedle>(), (int)((float)base.Projectile.damage * 0.8f), base.Projectile.knockBack, base.Projectile.owner);
					}
					shootingTimer = 0f;
				}
				else
				{
					base.Projectile.Kill();
				}
			}
		}
		if (base.Owner.altFunctionUse == 2 && calPlayer.unstableCastersGauntletVis >= 12f && base.Owner.ownedProjectileCounts[ModContent.ProjectileType<SigilSet>()] <= 0)
		{
			float sigilFireRate2 = (float)base.HeldItem.useTime * 4f;
			if (base.Owner.ownedProjectileCounts[ModContent.ProjectileType<SigilSet>()] <= 0 || shootingTimer >= sigilFireRate2)
			{
				calPlayer.unstableCastersGauntletVis -= 12f;
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MagicRockSound");
				style.Volume = 0.4f;
				style.PitchVariance = 0.05f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				if (Main.myPlayer == base.Projectile.owner)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Owner.Center, Vector2.Zero, ModContent.ProjectileType<SigilSet>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				}
				shootingTimer = 0f;
			}
		}
		else if (base.Owner.altFunctionUse == 2 && base.Owner.ownedProjectileCounts[ModContent.ProjectileType<SigilSet>()] == 1)
		{
			int parentType = ModContent.ProjectileType<SigilSet>();
			Projectile thisParent = null;
			ActiveEntityIterator<Projectile>.Enumerator enumerator2 = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				Projectile p2 = enumerator2.Current;
				if (p2.type == parentType && p2.owner == base.Projectile.owner)
				{
					thisParent = p2;
					break;
				}
			}
			if (thisParent != null)
			{
				int maxSigils = 6;
				bool[] slotIsOccupied = new bool[maxSigils];
				int nonConsumedSigils = 0;
				int[] sigilVariants = new int[7]
				{
					ModContent.ProjectileType<IgnisSigil>(),
					ModContent.ProjectileType<AquaSigil>(),
					ModContent.ProjectileType<TerraSigil>(),
					ModContent.ProjectileType<AerSigil>(),
					ModContent.ProjectileType<OrdoSigil>(),
					ModContent.ProjectileType<PerditoSigil>(),
					ModContent.ProjectileType<WarpSigil>()
				};
				for (int j = 0; j < Main.maxProjectiles; j++)
				{
					Projectile proj2 = Main.projectile[j];
					if (!proj2.active || proj2.owner != base.Projectile.owner || proj2.ai[0] != (float)thisParent.identity)
					{
						continue;
					}
					bool isASigil = false;
					int[] array = sigilVariants;
					foreach (int sigilType2 in array)
					{
						if (proj2.type == sigilType2)
						{
							isASigil = true;
							break;
						}
					}
					if (isASigil)
					{
						int sigilIndex = (int)proj2.ai[1];
						if (sigilIndex >= 0 && sigilIndex < maxSigils)
						{
							slotIsOccupied[sigilIndex] = true;
						}
						if (proj2.ai[2] <= 0f)
						{
							nonConsumedSigils++;
						}
					}
				}
				List<int> availableIndices = new List<int>();
				for (int l = 0; l < maxSigils; l++)
				{
					if (!slotIsOccupied[l])
					{
						availableIndices.Add(l);
					}
				}
				if (availableIndices.Count > 0 && nonConsumedSigils > 0 && calPlayer.unstableCastersGauntletVis >= 12f)
				{
					calPlayer.unstableCastersGauntletVis -= 12f;
					SoundStyle style = SoundID.DD2_EtherianPortalOpen with
					{
						Volume = 0.6f
					};
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					if (Main.myPlayer == base.Projectile.owner)
					{
						foreach (int sigilIndex2 in availableIndices)
						{
							Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), thisParent.Center, Vector2.Zero, ModContent.ProjectileType<WarpSigil>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, thisParent.identity, sigilIndex2);
						}
					}
					shootingTimer = 0f;
				}
			}
		}
		base.ExtraFrontArmRotation = currentRecoilRotation;
		if (currentRecoilRotation != 0f)
		{
			currentRecoilRotation = MathHelper.Lerp(currentRecoilRotation, 0f, RotationResolveSpeed);
		}
		shootingTimer++;
	}
}
