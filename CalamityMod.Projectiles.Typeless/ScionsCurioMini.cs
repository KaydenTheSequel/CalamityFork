using System;
using System.Collections.Generic;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Dusts;
using CalamityMod.Items.Accessories;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class ScionsCurioMini : ModProjectile, ILocalizedModType, IModType
{
	public Color usedColor;

	public float fxFade;

	public float followSpeed;

	public float lerpDir;

	public float facing;

	public Vector2 savedPos;

	public int swineSecretTimer;

	public int swineText;

	public float actionSpeed;

	public int chosenSecret;

	public int idleMax;

	private Vector2 goalPosition;

	public List<int> listNumbers;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float time => ref base.Projectile.ai[0];

	public ref float attackTimer => ref base.Projectile.ai[1];

	public ref float idleTimer => ref base.Projectile.localAI[0];

	public bool sharingSwineSecrets => idleTimer >= (float)idleMax;

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 38;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 300;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
	}

	public override void AI()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0776: Unknown result type (might be due to invalid IL or missing references)
		//IL_0781: Unknown result type (might be due to invalid IL or missing references)
		//IL_0840: Unknown result type (might be due to invalid IL or missing references)
		//IL_0846: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_069b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0700: Unknown result type (might be due to invalid IL or missing references)
		//IL_0706: Unknown result type (might be due to invalid IL or missing references)
		if (Main.zenithWorld)
		{
			idleMax = 301;
		}
		float rate = Main.GlobalTimeWrappedHourly * 5f;
		List<Color> eColors = new List<Color>
		{
			Color.Chartreuse,
			Color.LimeGreen
		};
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		usedColor = Color.Lerp(Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f)), Color.White, 0.7f);
		float sine = (float)Math.Sin(time * 0.1f * actionSpeed / (float)Math.PI);
		float sine2 = (float)Math.Sin(time * 0.15f * actionSpeed / (float)Math.PI);
		Vector2 baseDestination = Owner.Center - Vector2.UnitY * (20f + 5f * sine2) - Vector2.UnitX * 30f * lerpDir;
		if (Owner.Calamity().scionsCurioGotHit)
		{
			if (attackTimer == 0f)
			{
				savedPos = baseDestination;
			}
			if (attackTimer == 30f)
			{
				float blastScale = 1.6f;
				for (int g = 0; g < 17; g++)
				{
					int DustID = ModContent.DustType<SquashDust>();
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center, DustID);
					dust.scale = Main.rand.NextFloat(1.1f, 1.35f) * blastScale;
					dust.velocity = Utils.RotatedByRandom(new Vector2(9f, 9f), 100.0) * blastScale * Main.rand.NextFloat(0.4f, 0.9f) + Vector2.UnitY * -10f;
					dust.noGravity = false;
					dust.color = (Main.rand.NextBool() ? Color.Green : Color.Chartreuse);
					dust.fadeIn = Main.rand.NextFloat(0.2f, 2f);
				}
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Chartreuse * 0.9f, "CalamityMod/Particles/ShineExplosion1", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.05f * blastScale, 0.15f * blastScale, 10, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				SoundStyle style = SoundID.DD2_ExplosiveTrapExplode with
				{
					Volume = 0.5f * blastScale,
					Pitch = Main.rand.NextFloat(0.5f, 0.7f),
					MaxInstances = 6
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				float blastSize = 115f * blastScale;
				float minMultiplier = 0.5f;
				int hitsToMinMult = 5;
				int debuff = ModContent.BuffType<Irradiated>();
				int debuffTime = 300;
				Projectile projectile = Projectile.NewProjectileDirect(Owner.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BasicBurst>(), ScionsCurio.postHitDamage, 7f, (Owner != null) ? Owner.whoAmI : (-1), blastSize, minMultiplier, hitsToMinMult);
				projectile.localAI[0] = debuff;
				projectile.localAI[1] = debuffTime;
				projectile.timeLeft = 15;
				projectile.DamageType = DamageClass.Ranged;
				attackTimer = -1f;
				Owner.Calamity().scionsCurioGotHit = false;
			}
			baseDestination = savedPos + Utils.RotatedBy(new Vector2(4f, 4f), (double)(sine2 * 0.02f), default(Vector2));
			attackTimer++;
		}
		facing = Math.Sign((base.Projectile.Center.X == Owner.Center.X) ? ((float)Owner.direction) : base.Projectile.Center.DirectionTo(Owner.Center).X);
		Vector2 speakingDestination = Owner.Center - Vector2.UnitY * (10f + 3f * sine2) + Vector2.UnitX * 45f * lerpDir;
		if (time == 0f)
		{
			goalPosition = baseDestination;
		}
		goalPosition = Vector2.Lerp(goalPosition, (swineSecretTimer > 0) ? speakingDestination : baseDestination, 0.08f);
		lerpDir = MathHelper.Lerp((float)Owner.direction, facing, 0.04f);
		base.Projectile.velocity = (goalPosition - base.Projectile.Center) / (followSpeed * ((swineSecretTimer > 0) ? 0.5f : 1f));
		base.Projectile.rotation = 0.2f * sine;
		if (Owner.Calamity().scionsCurioVisuals)
		{
			Lighting.AddLight(base.Projectile.Center, ((Color)(ref usedColor)).ToVector3() * 0.3f);
		}
		int textSpeed = 4;
		if (swineSecretTimer > 0)
		{
			if (swineSecretTimer % textSpeed == 0 && swineSecretTimer > 30)
			{
				int originalLength = CalamityUtils.GetTextValue("Misc.SwineSecret" + chosenSecret).Length + 1;
				string text = CalamityUtils.GetTextValue("Misc.SwineSecret" + chosenSecret);
				int charstoRemove = text.Length - swineText;
				if (charstoRemove > -1)
				{
					text = text.Remove(swineText, charstoRemove);
					if (swineSecretTimer >= 2)
					{
						text = text.Remove(0, swineText - 1);
					}
				}
				if (swineText <= originalLength)
				{
					swineText++;
				}
				float textSpacing = Utils.Remap(originalLength, 5f, 190f, 20f, 9f);
				Vector2 position = Owner.Center - Vector2.UnitX * (float)originalLength * (textSpacing / 2f) + Vector2.UnitX * (float)originalLength * textSpacing / (float)originalLength * (float)swineText;
				int letter = CombatText.NewText(new Rectangle((int)position.X, (int)position.Y, 1, 1), usedColor, text);
				Main.combatText[letter].lifeTime = 100;
			}
			if (swineSecretTimer % 6 == 0 && swineSecretTimer > 30)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/Swine", 2);
				style.Volume = 0.6f;
				style.Pitch = Main.rand.NextFloat(-0.35f, 0.55f);
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				actionSpeed = Main.rand.NextFloat(0.5f, 1.5f);
			}
			swineSecretTimer--;
			fxFade = MathHelper.Lerp(fxFade, 1f, 0.08f);
			if (swineSecretTimer == 0)
			{
				time = 120f;
			}
		}
		else
		{
			swineText = 1;
			fxFade = MathHelper.Lerp(fxFade, 0f, 0.08f);
		}
		if (idleTimer % 90f == 0f && swineSecretTimer == 0 && idleTimer > (float)(idleMax - 300))
		{
			CombatText.NewText(base.Projectile.Hitbox, usedColor, CalamityUtils.GetTextValue("Misc.SwineSecret0"), dramatic: false, dot: true);
			actionSpeed = 1.1f;
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/Swine", 2);
			style.Volume = 0.6f;
			style.Pitch = Main.rand.NextFloat(0.35f, 0.55f);
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		actionSpeed = MathHelper.Lerp(actionSpeed, 1f, 0.08f);
		if (Owner.Calamity().scionsCurio)
		{
			base.Projectile.timeLeft++;
		}
		else
		{
			base.Projectile.Kill();
		}
		if (Owner.dead)
		{
			base.Projectile.Kill();
		}
		time++;
		if (sharingSwineSecrets && swineSecretTimer == 0)
		{
			int number = 0;
			for (int i = 0; i < 1; i++)
			{
				int attemptNumber = Main.rand.Next(1, 41);
				if (listNumbers.Contains(attemptNumber))
				{
					if (listNumbers.Count >= 40)
					{
						listNumbers.Clear();
						listNumbers.Add(attemptNumber);
						number = attemptNumber;
					}
					else
					{
						i--;
					}
				}
				else
				{
					listNumbers.Add(attemptNumber);
					number = attemptNumber;
				}
			}
			chosenSecret = number;
			int originalLength2 = CalamityUtils.GetTextValue("Misc.SwineSecret" + chosenSecret).Length;
			swineSecretTimer = originalLength2 * textSpeed + 30;
			idleTimer = idleMax - 300 - swineSecretTimer;
		}
		if (((Vector2)(ref Owner.velocity)).Length() < 2f && Owner.Calamity().scionsCurioVisuals)
		{
			idleTimer++;
		}
		else
		{
			idleTimer = 0f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		_ = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Texture2D cTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/HalfStar", (AssetRequestMode)2).Value;
		Color drawColor = usedColor * Utils.GetLerpValue(0f, 100f, time, clamped: true);
		Color bodyColor = lightColor;
		float drawMult = 1f;
		float attackFade = (float)Math.Pow(1f + (float)Math.Pow(Utils.GetLerpValue(0f, 30f, attackTimer, clamped: true), 4.0), 3.0);
		Vector2 shake = Main.rand.NextVector2Circular((attackFade - 1f) * 5f, (attackFade - 1f) * 5f);
		Color val;
		for (int i = 0; i < 18; i++)
		{
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 18f).ToRotationVector2() * 2f * drawMult * attackFade;
			if (attackFade > 1f || Owner.Calamity().scionsCurioVisuals)
			{
				Texture2D value = tex.Value;
				Vector2 position = base.Projectile.Center - Main.screenPosition + drawOffset + shake;
				val = Color.Lerp(drawColor, Color.Chartreuse, attackFade - 1f);
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(value, position, null, val * 0.2f * drawMult, base.Projectile.rotation, tex.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)(facing != -1f));
			}
		}
		if (Owner.Calamity().scionsCurioVisuals)
		{
			Texture2D value2 = tex.Value;
			Vector2 position2 = base.Projectile.Center - Main.screenPosition + shake;
			val = Color.Chartreuse;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value2, position2, null, Color.Lerp(bodyColor, val, attackFade - 1f), base.Projectile.rotation, tex.Size() * 0.5f, new Vector2(1f, 1f) * base.Projectile.scale, (SpriteEffects)(facing != -1f));
		}
		for (int j = -1; j <= 1; j += 2)
		{
			for (int k = -4; k <= 4; k++)
			{
				Vector2 pos = (Vector2.UnitX * 3f * facing + Vector2.UnitY * -2f).RotatedBy(base.Projectile.rotation);
				float scale = 1f;
				if (k == 0)
				{
					k++;
				}
				if (k > 0)
				{
					scale = 0.9f;
					pos = (Vector2.UnitX * 10f * facing + Vector2.UnitY * -2f).RotatedBy(base.Projectile.rotation);
				}
				Vector2 position3 = base.Projectile.Center - Main.screenPosition + pos + shake;
				val = Color.Lerp(Color.Red, Color.White, (float)Math.Abs(k) * 0.05f);
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(cTexture, position3, null, val * 0.7f * fxFade, base.Projectile.rotation + ((j == -1) ? ((float)Math.PI / 2f) : 0f), cTexture.Size() * 0.5f, new Vector2(2f - 0.1f * (float)Math.Abs(k), 1f + 0.4f * (float)Math.Abs(k)) * base.Projectile.scale * 0.35f * scale * Main.rand.NextFloat(0.65f, 1.25f), (SpriteEffects)0);
			}
		}
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public ScionsCurioMini()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		usedColor = Color.Chartreuse;
		followSpeed = 12f;
		swineText = 1;
		actionSpeed = 1f;
		idleMax = 10800;
		listNumbers = new List<int>();
		base._002Ector();
	}
}
