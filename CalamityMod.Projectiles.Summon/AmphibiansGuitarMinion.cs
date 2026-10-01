using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CalamityMod.Buffs.Summon;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class AmphibiansGuitarMinion : BaseMinionProjectile
{
	private bool _hasSpawned;

	private Color _effectsColor;

	public override int AssociatedProjectileTypeID => ModContent.ProjectileType<AmphibiansGuitarMinion>();

	public override int AssociatedBuffTypeID => ModContent.BuffType<AmphibiansGuitarBuff>();

	public override ref bool AssociatedMinionBool => ref base.ModdedOwner.AmphibiansGuitarBool;

	private int GuitarSprite
	{
		get
		{
			return (int)base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = MathHelper.Clamp(value, 0, 7);
		}
	}

	private ref float ShootTimer => ref base.Projectile.ai[1];

	private ref float ShootCount => ref base.Projectile.ai[2];

	private Vector2 RotationPosition
	{
		get
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			if (base.Target != null)
			{
				return base.Owner.MountedCenter;
			}
			return base.Owner.MountedCenter;
		}
	}

	private float IntendedRotationAngle => (float)Math.PI * 2f / ((base.Owner == null) ? 1f : MathHelper.Clamp((float)base.Owner.ownedProjectileCounts[base.Type], 1f, 8f)) * (float)GuitarSprite + Time * 2.4f;

	private static float Time => (float)Main.GameUpdateCount / 60f;

	public override void SetDefaults()
	{
		base.SetDefaults();
		Projectile projectile = base.Projectile;
		Projectile projectile2 = base.Projectile;
		projectile.width = 92;
		projectile2.height = 92;
	}

	public override void MinionAI()
	{
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0610: Unknown result type (might be due to invalid IL or missing references)
		//IL_0615: Unknown result type (might be due to invalid IL or missing references)
		//IL_0620: Unknown result type (might be due to invalid IL or missing references)
		//IL_0625: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_0630: Unknown result type (might be due to invalid IL or missing references)
		//IL_063e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0657: Unknown result type (might be due to invalid IL or missing references)
		//IL_0673: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		//IL_0558: Unknown result type (might be due to invalid IL or missing references)
		//IL_0568: Unknown result type (might be due to invalid IL or missing references)
		//IL_056d: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0578: Unknown result type (might be due to invalid IL or missing references)
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_059f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		if (!_hasSpawned)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/AmphibiansGuitarSummon");
			style.Volume = 0.8f;
			style.Pitch = ((GuitarSprite == 2 || GuitarSprite == 5) ? (-0.15f) : 0f);
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			ShootTimer = (float)GuitarSprite * 12f;
			_hasSpawned = true;
		}
		float oscillation = MathHelper.Clamp(Math.Abs((float)Math.Sin(Time * 5f / (float)Math.PI)), 0f, 1f);
		Vector2 rotationPosition = RotationPosition;
		Vector2 unitY = Vector2.UnitY;
		double radians = IntendedRotationAngle;
		Vector2 center = default(Vector2);
		Vector2 val = unitY.RotatedBy(radians, center);
		float num;
		if (base.Target != null)
		{
			center = base.Target.Size;
			num = ((Vector2)(ref center)).Length() / 2f + (600f - 350f * oscillation);
		}
		else
		{
			num = 100f;
		}
		Vector2 intendedPosition = rotationPosition - val * num;
		base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, intendedPosition, Utils.Remap(base.Projectile.DistanceSQ(intendedPosition), 6400f, 0f, 0.1f, 0.3f));
		base.Projectile.rotation = IntendedRotationAngle;
		if (base.Target == null)
		{
			return;
		}
		bool bigShot = ShootCount % 3f == 0f;
		if (ShootTimer > 96f && Main.myPlayer == base.Projectile.owner)
		{
			for (int i = 0; i < ((!bigShot) ? 1 : 3); i++)
			{
				float rot = i switch
				{
					2 => 0.25f, 
					1 => -0.25f, 
					_ => 0f, 
				};
				IEntitySource source_FromThis = base.Projectile.GetSource_FromThis();
				Vector2 center2 = base.Projectile.Center;
				Vector2 spinningpoint = CalamityUtils.CalculatePredictiveAimToTarget(base.Projectile.Center, base.Target, (bigShot ? 36f : 25f) * (1f - Math.Abs(rot)));
				double radians2 = rot;
				center = default(Vector2);
				Projectile.NewProjectile(source_FromThis, center2, spinningpoint.RotatedBy(radians2, center), ModContent.ProjectileType<AmphibiansGuitarProjectile>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, (Main.rand.NextBool() && base.Owner.ownedProjectileCounts[base.Type] == 8).ToInt(), Main.rand.Next(0, 5), bigShot ? 5 : 0);
			}
			if (bigShot)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/Evernote");
				style.Volume = 0.5f;
				style.Pitch = Main.rand.NextFloat(-0.1f, 0.1f);
				style.MaxInstances = 10;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, _effectsColor, "CalamityMod/Particles/HighResFoggyCircleHardEdge", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.01f, 0.09f, 17, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			else
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/WulfrumProsthesisShoot");
				style.Volume = 0.3f;
				style.Pitch = Main.rand.NextFloat(0.6f, 0.7f);
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			ShootCount++;
			ShootTimer = 0f;
		}
		if (ShootTimer < 10f)
		{
			Projectile projectile = base.Projectile;
			projectile.Center -= base.Projectile.Center.DirectionTo(base.Target.Center) * 10f;
		}
		float rate = Time * 2f;
		int num2 = 5;
		List<Color> list = new List<Color>(num2);
		CollectionsMarshal.SetCount(list, num2);
		Span<Color> span = CollectionsMarshal.AsSpan(list);
		int num3 = 0;
		span[num3] = Color.Red;
		num3++;
		span[num3] = Color.Cyan;
		num3++;
		span[num3] = Color.Goldenrod;
		num3++;
		span[num3] = Color.Magenta;
		num3++;
		span[num3] = Color.Lime;
		List<Color> eColors = list;
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		_effectsColor = Color.Lerp(Color.White, Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f)), 0.7f);
		if (Main.rand.NextBool(3))
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(70f, 70f), ModContent.DustType<LightDust>(), (base.Projectile.Center.DirectionTo(intendedPosition) * -9f).RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.3f, 1f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.75f, 1.25f);
			dust.color = _effectsColor;
			dust.noLightEmittence = true;
		}
		else
		{
			GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center + Main.rand.NextVector2Circular(70f, 70f), (base.Projectile.Center.DirectionTo(intendedPosition) * -9f).RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.3f, 1f), affectedByGravity: false, 7, Main.rand.NextFloat(0.5f, 0.8f), _effectsColor, AddativeBlend: true, needed: false, GlowCenter: false));
		}
		ShootTimer++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = texture.Frame(8, 1, GuitarSprite);
		Projectile projectile = base.Projectile;
		Color effectsColor = _effectsColor;
		((Color)(ref effectsColor)).A = 0;
		projectile.DrawProjectileWithBackglow(effectsColor, lightColor, (base.Target != null) ? 8 : 0, texture, frame, (SpriteEffects)0);
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, frame, Color.White, base.Projectile.rotation, frame.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public AmphibiansGuitarMinion()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		_effectsColor = Color.White;
		base._002Ector();
	}
}
