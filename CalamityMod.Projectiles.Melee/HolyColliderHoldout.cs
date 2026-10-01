using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

[PierceResistException(false)]
public class HolyColliderHoldout : BaseCustomUseStyleProjectile, ILocalizedModType, IModType
{
	public Vector2 mousePos;

	public Vector2 aimVel;

	public bool doSwing;

	public bool postSwing;

	public float fadeIn;

	public int useAnim;

	public int storedUseAnim;

	public int swingCount;

	public int pierceReduction;

	public bool chargedSwing;

	public int chargeTimer;

	public int chargeTimerMax;

	public Color mainColor1;

	public Color mainColor2;

	public bool playSwingSound;

	public SlotId AudSlot;

	public override int AssignedItemID => ModContent.ItemType<HolyCollider>();

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<HolyCollider>();

	public override string Texture => "CalamityMod/Items/Weapons/Melee/HolyCollider";

	public override float HitboxOutset => 100f;

	public override Vector2 HitboxSize
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(200f, 200f) * base.Projectile.scale;
		}
	}

	public override float HitboxRotationOffset => MathHelper.ToRadians(-45f);

	public override Vector2 SpriteOrigin
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(0f, 146f);
		}
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = TrueMeleeDamageClass.Instance;
	}

	public override void WhenSpawned()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.knockBack = 0f;
		base.Projectile.scale = 1f;
		base.Projectile.ai[1] = -1f;
		mousePos = Owner.Calamity().mouseWorld;
		aimVel = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
		useAnim = Owner.itemAnimationMax;
		storedUseAnim = useAnim;
		chargeTimerMax = (int)((float)useAnim * 1.1f);
		if (mousePos.X < Owner.Center.X)
		{
			Owner.direction = -1;
		}
		else
		{
			Owner.direction = 1;
		}
		FlipAsSword = Owner.direction == -1;
	}

	public override void UseStyle()
	{
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0787: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_080a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0848: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0611: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_062c: Unknown result type (might be due to invalid IL or missing references)
		//IL_062d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0632: Unknown result type (might be due to invalid IL or missing references)
		//IL_0637: Unknown result type (might be due to invalid IL or missing references)
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_0670: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08de: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_09dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0705: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b24: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bdc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c45: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_12dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_12fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1304: Unknown result type (might be due to invalid IL or missing references)
		//IL_130c: Unknown result type (might be due to invalid IL or missing references)
		//IL_131b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1334: Unknown result type (might be due to invalid IL or missing references)
		//IL_133a: Unknown result type (might be due to invalid IL or missing references)
		//IL_133b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1349: Unknown result type (might be due to invalid IL or missing references)
		//IL_134e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1358: Unknown result type (might be due to invalid IL or missing references)
		//IL_136e: Unknown result type (might be due to invalid IL or missing references)
		//IL_137b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f67: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f93: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d16: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_13bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ddb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d24: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d47: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1015: Unknown result type (might be due to invalid IL or missing references)
		//IL_119a: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_11bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1201: Unknown result type (might be due to invalid IL or missing references)
		//IL_1206: Unknown result type (might be due to invalid IL or missing references)
		//IL_1210: Unknown result type (might be due to invalid IL or missing references)
		//IL_1226: Unknown result type (might be due to invalid IL or missing references)
		//IL_1233: Unknown result type (might be due to invalid IL or missing references)
		//IL_1239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d82: Unknown result type (might be due to invalid IL or missing references)
		//IL_105d: Unknown result type (might be due to invalid IL or missing references)
		//IL_106c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1082: Unknown result type (might be due to invalid IL or missing references)
		//IL_1088: Unknown result type (might be due to invalid IL or missing references)
		//IL_1089: Unknown result type (might be due to invalid IL or missing references)
		//IL_1097: Unknown result type (might be due to invalid IL or missing references)
		//IL_109c: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_10bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_127c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_1281: Unknown result type (might be due to invalid IL or missing references)
		//IL_1693: Unknown result type (might be due to invalid IL or missing references)
		//IL_16a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_16bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_1100: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1700: Unknown result type (might be due to invalid IL or missing references)
		//IL_1705: Unknown result type (might be due to invalid IL or missing references)
		//IL_170a: Unknown result type (might be due to invalid IL or missing references)
		//IL_170f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1719: Unknown result type (might be due to invalid IL or missing references)
		//IL_171e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1723: Unknown result type (might be due to invalid IL or missing references)
		//IL_1728: Unknown result type (might be due to invalid IL or missing references)
		//IL_172a: Unknown result type (might be due to invalid IL or missing references)
		//IL_172f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1754: Unknown result type (might be due to invalid IL or missing references)
		//IL_1759: Unknown result type (might be due to invalid IL or missing references)
		//IL_175e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1772: Unknown result type (might be due to invalid IL or missing references)
		//IL_147c: Unknown result type (might be due to invalid IL or missing references)
		//IL_148b: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_14aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1504: Unknown result type (might be due to invalid IL or missing references)
		//IL_150f: Unknown result type (might be due to invalid IL or missing references)
		//IL_151f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1524: Unknown result type (might be due to invalid IL or missing references)
		//IL_1529: Unknown result type (might be due to invalid IL or missing references)
		//IL_152e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1538: Unknown result type (might be due to invalid IL or missing references)
		//IL_158f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1594: Unknown result type (might be due to invalid IL or missing references)
		//IL_1599: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ad: Unknown result type (might be due to invalid IL or missing references)
		AnimationProgress = Animation % (float)(chargedSwing ? ((int)((float)storedUseAnim * 1.2f)) : storedUseAnim);
		DrawUnconditionally = false;
		bool num = Owner.Calamity().mouseRight || Owner == null || !Owner.active || Owner.dead || Main.mouseLeftRelease || Owner.CCed || Owner.noItems;
		if (CanHit || postSwing)
		{
			mousePos = Owner.Center - aimVel;
		}
		else
		{
			mousePos = Owner.Calamity().mouseWorld;
		}
		if (CanHit)
		{
			fadeIn = MathHelper.Lerp(fadeIn, 1f, 0.1f);
		}
		else
		{
			fadeIn = MathHelper.Lerp(fadeIn, 0f, 0.15f);
		}
		if (chargeTimer > 0)
		{
			fadeIn = Utils.Remap(chargeTimer, 0f, chargeTimerMax, 0f, 1f);
		}
		if (num)
		{
			chargeTimer = 0;
			if (base.Projectile.ai[2] == 5f)
			{
				Owner.itemAnimation = Owner.itemAnimationMax;
				base.Projectile.timeLeft = Owner.itemAnimation;
			}
			base.Projectile.ai[2] = 0f;
		}
		else
		{
			base.Projectile.ai[2] = 5f;
		}
		if (!doSwing)
		{
			mousePos = Owner.Calamity().mouseWorld;
			aimVel = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
			CanHit = false;
			if (mousePos.X < Owner.Center.X)
			{
				Owner.direction = -1;
			}
			else
			{
				Owner.direction = 1;
			}
			FlipAsSword = Owner.direction == -1;
			Vector2 bladePos = default(Vector2);
			((Vector2)(ref bladePos))._002Ector(76f, 0f);
			Vector2 particlePos = Owner.Center + bladePos.RotatedBy(base.FinalRotation + MathHelper.ToRadians(-45f) - 0.1f * (float)(FlipAsSword ? 1 : (-1)) * (0f - base.Projectile.ai[1]));
			if (base.Projectile.ai[2] == 5f)
			{
				RotationOffset = MathHelper.Lerp(RotationOffset, MathHelper.ToRadians(120f * base.Projectile.ai[1] * (float)Owner.direction), 0.05f);
				float rotationValue = 45f + 25f * Utils.GetLerpValue(0f, chargeTimerMax, chargeTimer, clamped: true) * (float)(FlipAsSword ? 1 : (-1)) * (0f - base.Projectile.ai[1]);
				base.Projectile.rotation = base.Projectile.rotation.AngleLerp(Owner.AngleTo(mousePos) + MathHelper.ToRadians(rotationValue), 0.3f);
				Animation = 0f;
				Owner.itemAnimation++;
				base.Projectile.timeLeft++;
				if (chargeTimer < chargeTimerMax && !chargedSwing)
				{
					chargeTimer++;
				}
				Vector2 particleVel = (Owner.Center - particlePos).SafeNormalize(Vector2.UnitX) * -9f;
				Dust dust = Dust.NewDustPerfect(particlePos, ModContent.DustType<LightDust>(), particleVel.RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(0.2f, 1f));
				dust.scale = Main.rand.NextFloat(0.95f, 1.45f) * fadeIn;
				dust.noGravity = true;
				dust.color = (Main.rand.NextBool(3) ? mainColor2 : mainColor1);
				GeneralParticleHandler.SpawnParticle(new LineParticle(particlePos, particleVel.RotatedByRandom(100.0) * Main.rand.NextFloat(0.3f, 0.9f), affectedByGravity: false, 18, Main.rand.NextFloat(0.5f, 0.8f) * fadeIn, Main.rand.NextBool(3) ? mainColor2 : mainColor1));
				if (SoundEngine.TryGetActiveSound(AudSlot, out ActiveSound ChargeSound) && ChargeSound.IsPlaying)
				{
					ChargeSound.Position = base.Projectile.Center;
					ChargeSound.Pitch = Utils.Remap(chargeTimer, 0f, chargeTimerMax, -0.4f, 0f);
					ChargeSound.Volume = Utils.Remap(chargeTimer, 0f, chargeTimerMax, 0f, 0.5f) * 100f;
				}
				else if (!chargedSwing)
				{
					SoundStyle burn = new SoundStyle("CalamityMod/Sounds/Custom/Providence/ProvidenceBurnLoop");
					SoundStyle style = burn with
					{
						Volume = 0.01f,
						Pitch = 0f,
						IsLooped = true
					};
					AudSlot = SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
			}
			if (chargeTimer == chargeTimerMax)
			{
				particlePos = Owner.Center + bladePos.RotatedBy(base.FinalRotation + MathHelper.ToRadians(-45f));
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/Providence/ProvidenceBurn");
				style.Volume = 0.7f;
				style.Pitch = 0.5f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				chargedSwing = true;
				useAnim = storedUseAnim / 2;
				chargeTimer++;
				for (int i = 0; i < 20; i++)
				{
					GeneralParticleHandler.SpawnParticle(new LineParticle(particlePos, Utils.RotatedByRandom(new Vector2(8f, 8f), 100.0) * Main.rand.NextFloat(0.5f, 1f), affectedByGravity: false, 30, Main.rand.NextFloat(0.3f, 0.8f), Main.rand.NextBool(3) ? mainColor2 : mainColor1));
				}
			}
			if (chargeTimer == 0)
			{
				for (int j = 0; j < Main.maxNPCs; j++)
				{
					base.Projectile.localNPCImmunity[j] = 0;
				}
				base.Projectile.numHits = 0;
				pierceReduction = 0;
				doSwing = true;
			}
		}
		else if (chargeTimer == 0)
		{
			if (SoundEngine.TryGetActiveSound(AudSlot, out ActiveSound ChargeSound2))
			{
				ChargeSound2?.Stop();
			}
			if (!CanHit && !postSwing)
			{
				if (mousePos.X < Owner.Center.X)
				{
					Owner.direction = -1;
				}
				else
				{
					Owner.direction = 1;
				}
			}
			else if ((Owner.Center - aimVel).X < Owner.Center.X)
			{
				Owner.direction = -1;
			}
			else
			{
				Owner.direction = 1;
			}
			base.Projectile.rotation = base.Projectile.rotation.AngleLerp(Owner.AngleTo(mousePos) + MathHelper.ToRadians(45f), 0.1f);
			if (AnimationProgress < (float)useAnim / 1.6f)
			{
				if (base.Projectile.ai[2] == 5f && !chargedSwing)
				{
					doSwing = false;
				}
				playSwingSound = true;
				aimVel = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
				CanHit = false;
				postSwing = false;
				if (AnimationProgress == 0f)
				{
					base.Projectile.scale = 1f;
					Animation = 0f;
					doSwing = false;
					chargeTimer = 0;
					chargedSwing = false;
					swingCount++;
					useAnim = storedUseAnim;
					base.Projectile.ai[1] = 0f - base.Projectile.ai[1];
				}
				RotationOffset = MathHelper.Lerp(RotationOffset, MathHelper.ToRadians(120f * base.Projectile.ai[1] * (float)Owner.direction * (1f + Utils.GetLerpValue((float)useAnim * 0.15f, (float)useAnim * 0.35f, Animation, clamped: true) * 0.35f)), 0.2f);
				FlipAsSword = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX).X > 0f;
			}
			else
			{
				float time = AnimationProgress - (float)(useAnim / 3);
				float timeMax = useAnim - useAnim / 3;
				if (time >= (float)(int)(timeMax * (chargedSwing ? 0.2f : 0.4f)) && playSwingSound)
				{
					if (!chargedSwing)
					{
						SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/Providence/ProvidenceHolyBlastShoot");
						style.Volume = 0.7f;
						style.Pitch = 0.35f;
						SoundEngine.PlaySound(in style, base.Projectile.Center);
						style = new SoundStyle("CalamityMod/Sounds/Item/SwingMid");
						style.Volume = 0.55f;
						style.Pitch = -0.15f;
						SoundEngine.PlaySound(in style, base.Projectile.Center);
					}
					else
					{
						SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/ProfanedGuardians/GuardianShieldDeactivate");
						style.Volume = 0.8f;
						style.Pitch = -0.35f;
						SoundEngine.PlaySound(in style, base.Projectile.Center);
						style = new SoundStyle("CalamityMod/Sounds/Item/SwingMid");
						style.Volume = 0.9f;
						style.Pitch = -0.55f;
						SoundEngine.PlaySound(in style, base.Projectile.Center);
					}
					playSwingSound = false;
				}
				if (time > (float)(int)(timeMax * (chargedSwing ? 0.1f : 0.3f)) && time < (float)(int)(timeMax * (chargedSwing ? 0.95f : 0.85f)))
				{
					CanHit = true;
					Vector2 particleVel2 = Utils.RotatedBy(new Vector2(0f, 2f * (0f - base.Projectile.ai[1]) * (float)Owner.direction), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2));
					Vector2 particlePos2 = Owner.Center + Utils.RotatedBy(new Vector2((float)Main.rand.Next(30, 170), 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2));
					if (!chargedSwing)
					{
						for (int k = 0; k < 3; k++)
						{
							particleVel2 = (new Vector2(0f, 15f * (0f - base.Projectile.ai[1]) * (float)Owner.direction) * Main.rand.NextFloat(0.3f, 1f)).RotatedBy(base.FinalRotation + MathHelper.ToRadians(-45f));
							particlePos2 = Owner.Center + Utils.RotatedBy(new Vector2((float)Main.rand.Next(30, 170), 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2));
							if (k < 2)
							{
								Dust dust2 = Dust.NewDustPerfect(particlePos2, ModContent.DustType<LightDust>(), -particleVel2.RotatedByRandom(0.30000001192092896));
								dust2.scale = Main.rand.NextFloat(0.95f, 1.45f);
								dust2.noGravity = true;
								dust2.color = (Main.rand.NextBool(3) ? mainColor2 : mainColor1);
							}
							else
							{
								GeneralParticleHandler.SpawnParticle(new CustomSpark(particlePos2, (-particleVel2 * 0.2f).RotatedByRandom(0.30000001192092896), "CalamityMod/Particles/ProvidenceMarkParticle", affectedByGravity: false, 17, Main.rand.NextFloat(0.9f, 1.1f), Color.Lerp(Color.Orchid, Color.White, Main.rand.NextFloat(0f, 0.7f)), new Vector2(1.3f, 0.5f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, Main.rand.NextFloat(0.1f, 0.2f)));
							}
						}
					}
				}
				else
				{
					CanHit = false;
				}
				RotationOffset = MathHelper.Lerp(RotationOffset, MathHelper.ToRadians(MathHelper.Lerp(150f * base.Projectile.ai[1] * (float)Owner.direction, 120f * (0f - base.Projectile.ai[1]) * (float)Owner.direction, CalamityUtils.ExpInOutEasing(time / timeMax, 1))), 0.2f);
				if (time < (float)(int)(timeMax * 0.9f))
				{
					postSwing = true;
				}
				if (CanHit)
				{
					if (chargedSwing)
					{
						for (int l = 0; l < 6; l++)
						{
							float randRot = Main.rand.NextFloat(-10f, -45f);
							Vector2 dustVel = Utils.RotatedBy(new Vector2(0f, 15f * (0f - base.Projectile.ai[1]) * (float)Owner.direction), (double)(base.FinalRotation + MathHelper.ToRadians(randRot)), default(Vector2));
							GeneralParticleHandler.SpawnParticle(new CustomSpark(Owner.Center + Utils.RotatedBy(new Vector2(170f, 0f), (double)(base.FinalRotation + MathHelper.ToRadians(randRot)), default(Vector2)).RotatedByRandom(0.4000000059604645), -dustVel * Main.rand.NextFloat(0.4f, 0.7f), "CalamityMod/Particles/LargeBloom", affectedByGravity: false, Main.rand.Next(7, 10), Main.rand.NextFloat(0.3f, 0.35f), Main.rand.NextBool(4) ? Color.DarkGoldenrod : Color.Goldenrod, new Vector2(1f, 1.2f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.45f));
							if (l % 3 == 0)
							{
								GeneralParticleHandler.SpawnParticle(new CustomSpark(Owner.Center + Utils.RotatedBy(new Vector2(170f, 0f), (double)(base.FinalRotation + MathHelper.ToRadians(randRot)), default(Vector2)).RotatedByRandom(0.4000000059604645), -dustVel * Main.rand.NextFloat(0.4f, 0.7f), "CalamityMod/Particles/ProvidenceMarkParticle", affectedByGravity: false, 27, Main.rand.NextFloat(1.15f, 1.3f), Main.rand.NextBool(4) ? Color.Khaki : Color.Orange, new Vector2(1.3f, 0.5f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, Main.rand.NextFloat(0.1f, 0.2f)));
							}
						}
						for (int m = 0; m < 6; m++)
						{
							float randRot2 = Main.rand.NextFloat(-30f, -60f);
							Vector2 dustVel2 = Utils.RotatedBy(new Vector2(0f, 35f * (0f - base.Projectile.ai[1]) * (float)Owner.direction), (double)(base.FinalRotation + MathHelper.ToRadians(randRot2)), default(Vector2));
							Dust dust3 = Dust.NewDustPerfect(Owner.Center + Utils.RotatedBy(new Vector2(170f, 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2)).RotatedByRandom(0.30000001192092896), ModContent.DustType<LightDust>(), dustVel2 * Main.rand.NextFloat(0.3f, 0.9f));
							dust3.scale = Main.rand.NextFloat(0.95f, 1.45f);
							dust3.noGravity = true;
							dust3.color = (Main.rand.NextBool(3) ? mainColor2 : mainColor1);
						}
					}
					else
					{
						for (int n = 0; n < 8; n++)
						{
							float randRot3 = Main.rand.NextFloat(-30f, -60f);
							Vector2 dustVel3 = -Utils.RotatedBy(new Vector2(0f, 25f * (0f - base.Projectile.ai[1]) * (float)Owner.direction), (double)(base.FinalRotation + MathHelper.ToRadians(randRot3)), default(Vector2));
							Dust dust4 = Dust.NewDustPerfect(Owner.Center + Utils.RotatedBy(new Vector2(170f, 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2)).RotatedByRandom(0.15000000596046448), ModContent.DustType<LightDust>(), dustVel3 * Main.rand.NextFloat(0.1f, 0.5f));
							dust4.scale = Main.rand.NextFloat(0.75f, 0.9f);
							dust4.noGravity = true;
							dust4.color = (Main.rand.NextBool(3) ? mainColor2 : mainColor1);
						}
					}
					if (chargedSwing)
					{
						for (int x = 0; x < Main.maxProjectiles; x++)
						{
							Projectile projectile = Main.projectile[x];
							bool isProviProj = projectile.type == ModContent.ProjectileType<HolyFire>() || projectile.type == ModContent.ProjectileType<HolyFire2>() || projectile.type == ModContent.ProjectileType<HolyFlare>() || projectile.type == ModContent.ProjectileType<HolyBomb>() || projectile.type == ModContent.ProjectileType<HolyBlast>();
							bool isAFireball = (projectile.active && projectile.type == ModContent.ProjectileType<HolyColliderHolyFire>() && projectile.ai[0] != 10f) | isProviProj;
							if (!((Vector2.Distance(Owner.Center + Utils.RotatedBy(new Vector2(60f, 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2)), projectile.Center) <= (float)(150 + Math.Max(projectile.width, projectile.height)) && projectile.active) & isAFireball))
							{
								continue;
							}
							if (isProviProj)
							{
								Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), projectile.Center, (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitY) * -2.5f, ModContent.ProjectileType<HolyColliderHolyFire>(), (int)((double)base.Projectile.damage * 0.1 + (double)projectile.damage), base.Projectile.knockBack, base.Projectile.owner, 0f, 15f, 5f);
								GeneralParticleHandler.SpawnParticle(new CustomPulse(projectile.Center, Vector2.Zero, Color.Goldenrod, "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 0.8f, 11, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
								if (projectile.type == ModContent.ProjectileType<HolyBlast>())
								{
									projectile.active = false;
								}
								else
								{
									projectile.Kill();
								}
							}
							else
							{
								projectile.ai[2] = 5f;
								projectile.owner = Owner.whoAmI;
							}
						}
					}
					else
					{
						for (int num2 = 0; num2 < Main.maxProjectiles; num2++)
						{
							Projectile projectile2 = Main.projectile[num2];
							bool isAFireball2 = projectile2.active && projectile2.type == ModContent.ProjectileType<HolyColliderHolyFire>() && projectile2.ai[0] != 10f;
							if ((Vector2.Distance(Owner.Center + Utils.RotatedBy(new Vector2(60f, 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2)), projectile2.Center) <= 150f) & isAFireball2)
							{
								Vector2 launch = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitY) * -2.5f;
								projectile2.velocity += launch;
								projectile2.timeLeft = 300;
								projectile2.owner = Owner.whoAmI;
								GeneralParticleHandler.SpawnParticle(new CustomPulse(projectile2.Center, Vector2.Zero, Color.Goldenrod, "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 0.8f, 11, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
							}
						}
					}
				}
			}
		}
		ArmRotationOffset = MathHelper.ToRadians(-140f);
		ArmRotationOffsetBack = MathHelper.ToRadians(-140f);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(AudSlot, out ActiveSound ChargeSound))
		{
			ChargeSound?.Stop();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 90);
		if ((damageDone <= 2 || (target.life <= 0 && target.realLife == -1)) && pierceReduction > 0)
		{
			pierceReduction--;
		}
		if (!chargedSwing)
		{
			if (base.Projectile.numHits == 0)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/HolyColliderSmallHit");
				style.Volume = 0.85f;
				style.PitchVariance = 0.25f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				for (int i = 0; i < 8; i++)
				{
					Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), target.Center, ((Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitY) * -11f).RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(0.3f, 1f), ModContent.ProjectileType<HolyColliderHolyFire>(), (int)((double)base.Projectile.damage * 0.05), base.Projectile.knockBack, base.Projectile.owner, 10f, target.whoAmI).timeLeft = Main.rand.Next(40, 56);
				}
			}
		}
		else if (base.Projectile.numHits == 0)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/HolyColliderBigHit");
			style.Volume = 1f;
			style.PitchVariance = 0.15f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			float starAngle = MathHelper.ToRadians(45f);
			for (int j = 0; j < 4; j++)
			{
				Dust.NewDustPerfect(base.Projectile.Center, 278);
				Vector2 vel = ((float)Math.PI * 2f * (float)j / 4f).ToRotationVector2().RotatedBy(starAngle) * 4f;
				GeneralParticleHandler.SpawnParticle(new CustomSpark(target.Center, vel, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 12, 1.2f, Color.Orange, new Vector2(3.2f, 0.9f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.95f));
			}
			for (int k = 0; k < 4; k++)
			{
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), target.Center, Utils.RotatedBy(new Vector2(0f, -35f), (double)MathHelper.ToRadians(45f), default(Vector2)).RotatedBy(MathHelper.ToRadians(90f) * (float)k), ModContent.ProjectileType<HolyColliderHolyFire>(), (int)((double)base.Projectile.damage * 0.1), base.Projectile.knockBack, base.Projectile.owner);
			}
		}
		Vector2 launchVel = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld);
		target.MoveNPC(launchVel, chargedSwing ? 35 : 23, ignoreKBImmune: true);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float minMult = 0.25f;
		int hitsToMinMult = 10;
		float damageMult = Utils.Remap(pierceReduction, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= (chargedSwing ? 1f : 0.5f) * damageMult;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		if ((useAnim > 0 || DrawUnconditionally) && Owner.ItemAnimationActive)
		{
			Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
			Asset<Texture2D> glowTex = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/HolyColliderGlow", (AssetRequestMode)2);
			ModContent.Request<Texture2D>("CalamityMod/Particles/Sparkle", (AssetRequestMode)2);
			bool flipAsSword = ((swingCount % 2 == 0) ? (!FlipAsSword) : FlipAsSword);
			float r = (flipAsSword ? MathHelper.ToRadians(90f) : 0f);
			Vector2 generalDrawPos = base.Projectile.Center - Main.screenPosition + new Vector2(0f, Owner.gfxOffY);
			SpriteEffects sEffects = (SpriteEffects)(((int)spriteEffects == 0) ? (flipAsSword ? 1 : 0) : ((int)spriteEffects));
			for (int i = 0; i < 25; i++)
			{
				Texture2D centerTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/HolyColliderGhost", (AssetRequestMode)2).Value;
				Color val = mainColor1;
				((Color)(ref val)).A = 0;
				Color auraColor = val * 0.15f * fadeIn;
				Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 25f).ToRotationVector2() * 6f * fadeIn;
				Main.EntitySpriteDraw(centerTexture, base.Projectile.Center - Main.screenPosition + drawOffset + new Vector2(0f, Owner.gfxOffY), centerTexture.Frame(1, FrameCount, 0, Frame), auraColor, base.Projectile.rotation + RotationOffset + r, (Vector2)(flipAsSword ? new Vector2((float)tex.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, (SpriteEffects)(((int)spriteEffects != 0) ? ((int)spriteEffects) : (flipAsSword ? 1 : 0)));
			}
			Main.EntitySpriteDraw(tex.Value, generalDrawPos, tex.Frame(1, FrameCount, 0, Frame), lightColor, base.Projectile.rotation + RotationOffset + r, (Vector2)(flipAsSword ? new Vector2((float)tex.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, sEffects);
			Main.EntitySpriteDraw(glowTex.Value, generalDrawPos, glowTex.Frame(1, FrameCount, 0, Frame), Color.White, base.Projectile.rotation + RotationOffset + r, (Vector2)(flipAsSword ? new Vector2((float)glowTex.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, sEffects);
		}
		else
		{
			chargeTimer = 0;
			chargedSwing = false;
		}
		return false;
	}

	public override void ResetStyle()
	{
	}

	public HolyColliderHoldout()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		swingCount = -1;
		chargeTimerMax = 240;
		mainColor1 = Color.Goldenrod;
		mainColor2 = Color.OrangeRed;
		playSwingSound = true;
		base._002Ector();
	}
}
