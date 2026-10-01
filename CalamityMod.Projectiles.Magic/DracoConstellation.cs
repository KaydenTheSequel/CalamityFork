using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Utilities.Daybreak;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class DracoConstellation : ModProjectile, ILocalizedModType, IModType
{
	public class DracoSegment
	{
		public Vector2 Center;

		public float rotation;

		public Vector2 velocity;

		public Vector2 size;

		public float followDistance;

		public float PoseRotation;

		public DracoSegment(Projectile Head, float followingDistance = 50f)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0051: Unknown result type (might be due to invalid IL or missing references)
			Center = Vector2.Zero;
			velocity = Vector2.Zero;
			size = Vector2.One;
			followDistance = 50f;
			base._002Ector();
			Center = Head.Center;
			rotation = Head.rotation;
			velocity = Head.velocity;
			followDistance = followingDistance;
		}

		public DracoSegment(DracoConstellation Head, float followingDistance, float rotation)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_0062: Unknown result type (might be due to invalid IL or missing references)
			//IL_0067: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			Center = Vector2.Zero;
			velocity = Vector2.Zero;
			size = Vector2.One;
			followDistance = 50f;
			base._002Ector();
			DracoSegment prior = Head.Segments.LastOrDefault(new DracoSegment(Head.Projectile));
			Center = prior.Center + rotation.ToRotationVector2() * (followingDistance * 600f);
			this.rotation = prior.rotation;
			velocity = prior.velocity;
			followDistance = followingDistance;
			PoseRotation = rotation;
		}
	}

	public List<DracoSegment> Segments = new List<DracoSegment>();

	private static Asset<Texture2D> GlowTex;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Particles/Sparkle";

	public float TailLength => 600f;

	public static Texture2D GetGlowTex()
	{
		if (GlowTex == null)
		{
			GlowTex = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
		}
		return GlowTex.Value;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 64;
		base.Projectile.height = 64;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.extraUpdates = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.localNPCHitCooldown = 15 * base.Projectile.MaxUpdates;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.timeLeft = 300;
	}

	public override void AI()
	{
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0697: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05db: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0646: Unknown result type (might be due to invalid IL or missing references)
		//IL_0656: Unknown result type (might be due to invalid IL or missing references)
		//IL_065b: Unknown result type (might be due to invalid IL or missing references)
		//IL_065d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0662: Unknown result type (might be due to invalid IL or missing references)
		//IL_0669: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0673: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0613: Unknown result type (might be due to invalid IL or missing references)
		//IL_0619: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0620: Unknown result type (might be due to invalid IL or missing references)
		//IL_0622: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_063d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		if (Main.player[base.Projectile.owner].Calamity().StratusStarburst < 1 && base.Projectile.timeLeft > 60)
		{
			base.Projectile.timeLeft = 60;
			return;
		}
		if (base.Projectile.FinalExtraUpdate() && Main.player[base.Projectile.owner].miscCounter % 15 == 0 && base.Projectile.timeLeft > 60)
		{
			Main.player[base.Projectile.owner].Calamity().StratusStarburst--;
			Main.player[base.Projectile.owner].Calamity().StratusStarburstResetTimer = (int)MathHelper.Max((float)Main.player[base.Projectile.owner].Calamity().StratusStarburstResetTimer, 240f);
		}
		if (Segments.Count <= 0)
		{
			Segments = new List<DracoSegment>();
			Segments.Add(new DracoSegment(this, 0.1985f, 4.649557f));
			Segments.Add(new DracoSegment(this, 0.0835f, (float)Math.PI / 50f));
			Segments.Add(new DracoSegment(this, 0.1209f, (float)Math.PI * 2f / 5f));
			Segments.Add(new DracoSegment(this, 0.0938f, 0.9424779f));
			Segments.Add(new DracoSegment(this, 0.0571f, 0.9424779f));
			Segments.Add(new DracoSegment(this, 0.0711f, 6.2203536f));
			Segments.Add(new DracoSegment(this, 0.156f, 5.6548667f));
			Segments.Add(new DracoSegment(this, 0.1458f, 5.340708f));
			Segments.Add(new DracoSegment(this, 0.0733f, 5.6548667f));
		}
		if (base.Projectile.timeLeft <= 240 && base.Projectile.timeLeft > 60)
		{
			base.Projectile.timeLeft++;
			NPC target = null;
			if (base.Projectile.ai[2] > 0f)
			{
				target = Main.npc[(int)base.Projectile.ai[2]];
			}
			else if (Main.player[base.Projectile.owner].HasMinionAttackTargetNPC)
			{
				target = Main.npc[Main.player[base.Projectile.owner].MinionAttackTargetNPC];
			}
			float targetDistance = 1600f;
			if (target == null || !target.CanBeChasedBy(base.Projectile))
			{
				target = null;
			}
			if (target == null)
			{
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC item = enumerator.Current;
					if (item.CanBeChasedBy(base.Projectile) && base.Projectile.localNPCImmunity[item.whoAmI] <= 0 && !(item.Distance(Main.player[base.Projectile.owner].Center) > 1200f))
					{
						float dis = base.Projectile.Distance(item.Center);
						if (dis < targetDistance)
						{
							targetDistance = dis;
							target = item;
						}
					}
				}
			}
			if (target != null)
			{
				((Vector2)(ref base.Projectile.velocity)).Length();
				Projectile projectile = base.Projectile;
				projectile.velocity += base.Projectile.DirectionTo(target.Center) * 0.5f;
				base.Projectile.ai[2] = target.whoAmI + 1;
			}
			else
			{
				base.Projectile.ai[2] = 0f;
				if (base.Projectile.Distance(Main.player[base.Projectile.owner].Center) > 300f)
				{
					((Vector2)(ref base.Projectile.velocity)).Length();
					Projectile projectile2 = base.Projectile;
					projectile2.velocity += base.Projectile.DirectionTo(Main.player[base.Projectile.owner].Center) * 0.5f;
				}
			}
			if (((Vector2)(ref base.Projectile.velocity)).Length() > 5f)
			{
				Projectile projectile3 = base.Projectile;
				projectile3.velocity *= 0.98f;
			}
			if (base.Projectile.velocity != Vector2.Zero)
			{
				base.Projectile.rotation = base.Projectile.velocity.ToRotation();
			}
		}
		else if (base.Projectile.timeLeft > 60)
		{
			base.Projectile.rotation = (float)Math.PI / 2f;
			base.Projectile.scale = MathHelper.Lerp(1f, 0f, (float)(base.Projectile.timeLeft - 240) / 60f);
		}
		else
		{
			base.Projectile.scale = MathHelper.Lerp(0f, 1f, (float)base.Projectile.timeLeft / 60f);
			Projectile projectile4 = base.Projectile;
			projectile4.velocity *= 0.95f;
		}
		Projectile projectile5 = base.Projectile;
		projectile5.position += base.Projectile.velocity;
		if (base.Projectile.timeLeft <= 241 && base.Projectile.velocity != Vector2.Zero)
		{
			for (int i = 0; i < Segments.Count; i++)
			{
				float segmentDistance = Segments[i].followDistance * TailLength;
				DracoSegment thisSeg = Segments[i];
				DracoSegment aheadSeg = new DracoSegment(base.Projectile);
				if (i != 0)
				{
					aheadSeg = Segments[i - 1];
				}
				float intensity = 0.05f;
				Vector2 nexSegDir = aheadSeg.Center - thisSeg.Center;
				if (aheadSeg.rotation != thisSeg.rotation)
				{
					nexSegDir = nexSegDir.RotatedBy(MathHelper.WrapAngle(aheadSeg.rotation - thisSeg.rotation) * intensity);
					nexSegDir = nexSegDir.MoveTowards((aheadSeg.rotation - thisSeg.rotation).ToRotationVector2(), 1f);
				}
				thisSeg.rotation = nexSegDir.ToRotation();
				thisSeg.Center = aheadSeg.Center - nexSegDir.SafeNormalize(Vector2.Zero) * segmentDistance;
			}
		}
		Projectile projectile6 = base.Projectile;
		projectile6.position -= base.Projectile.velocity;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < Segments.Count; i++)
		{
			DracoSegment prevSeg = new DracoSegment(base.Projectile);
			if (i != 0)
			{
				prevSeg = Segments[i - 1];
			}
			float cpoint = 0f;
			if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), prevSeg.Center, Segments[i].Center, 4f, ref cpoint))
			{
				return true;
			}
		}
		return base.Colliding(projHitbox, targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		using (Main.spriteBatch.Scope())
		{
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.Transform);
			Texture2D tex = TextureAssets.Projectile[base.Type].Value;
			Color connectioncolor = Color.SkyBlue * ((MathF.Sin(Main.GlobalTimeWrappedHourly) + 1f) * 0.125f + 0.75f);
			for (int i = 0; i < Segments.Count; i++)
			{
				if (i == 0)
				{
					Main.spriteBatch.DrawLineBetter(base.Projectile.Center, Segments[i].Center, connectioncolor, 2f * base.Projectile.scale);
				}
				if (Segments.IndexInRange(i + 1))
				{
					DracoSegment nextSegment = Segments[i + 1];
					Main.spriteBatch.DrawLineBetter(Segments[i].Center, nextSegment.Center, connectioncolor, 2f * base.Projectile.scale);
				}
				Main.spriteBatch.Draw(GetGlowTex(), Segments[i].Center - Main.screenPosition, (Rectangle?)null, Color.SkyBlue, 0f, GetGlowTex().Size() * 0.5f, 0.2f * base.Projectile.scale, (SpriteEffects)0, 0f);
				Main.spriteBatch.Draw(tex, Segments[i].Center - Main.screenPosition, (Rectangle?)null, Color.White, 0f * Segments[i].followDistance * 5f * Main.GlobalTimeWrappedHourly, tex.Size() * 0.5f, 0.75f * base.Projectile.scale, (SpriteEffects)0, 0f);
			}
			connectOffsets(default(Vector2), new Vector2(-0.0425f, 0.0637f));
			connectOffsets(default(Vector2), new Vector2(0.0307f, 0.0418f));
			connectOffsets(new Vector2(-0.0425f, 0.0637f), new Vector2(0.0168f, 0.0791f));
			connectOffsets(new Vector2(0.0307f, 0.0418f), new Vector2(0.0168f, 0.0791f));
			DrawHeadStar(default(Vector2));
			DrawHeadStar(new Vector2(-0.0425f, 0.0637f));
			DrawHeadStar(new Vector2(0.0307f, 0.0418f));
			DrawHeadStar(new Vector2(0.0168f, 0.0791f));
			Main.spriteBatch.End();
			void connectOffsets(Vector2 offset1, Vector2 offset2)
			{
				//IL_000b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0010: Unknown result type (might be due to invalid IL or missing references)
				//IL_0025: Unknown result type (might be due to invalid IL or missing references)
				//IL_002b: Unknown result type (might be due to invalid IL or missing references)
				//IL_002c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0037: Unknown result type (might be due to invalid IL or missing references)
				//IL_003c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0047: Unknown result type (might be due to invalid IL or missing references)
				//IL_004c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0061: Unknown result type (might be due to invalid IL or missing references)
				//IL_0067: Unknown result type (might be due to invalid IL or missing references)
				//IL_0068: Unknown result type (might be due to invalid IL or missing references)
				//IL_0073: Unknown result type (might be due to invalid IL or missing references)
				//IL_0078: Unknown result type (might be due to invalid IL or missing references)
				//IL_007e: Unknown result type (might be due to invalid IL or missing references)
				Main.spriteBatch.DrawLineBetter(base.Projectile.Center + offset1.RotatedBy(base.Projectile.rotation - (float)Math.PI / 2f) * TailLength, base.Projectile.Center + offset2.RotatedBy(base.Projectile.rotation - (float)Math.PI / 2f) * TailLength, connectioncolor, 2f * base.Projectile.scale);
			}
			void DrawHeadStar(Vector2 offsetPercentage)
			{
				//IL_0010: Unknown result type (might be due to invalid IL or missing references)
				//IL_0015: Unknown result type (might be due to invalid IL or missing references)
				//IL_002a: Unknown result type (might be due to invalid IL or missing references)
				//IL_0030: Unknown result type (might be due to invalid IL or missing references)
				//IL_0031: Unknown result type (might be due to invalid IL or missing references)
				//IL_003c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0041: Unknown result type (might be due to invalid IL or missing references)
				//IL_0046: Unknown result type (might be due to invalid IL or missing references)
				//IL_004b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0059: Unknown result type (might be due to invalid IL or missing references)
				//IL_0068: Unknown result type (might be due to invalid IL or missing references)
				//IL_0072: Unknown result type (might be due to invalid IL or missing references)
				//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
				//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
				//IL_00be: Unknown result type (might be due to invalid IL or missing references)
				//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
				//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
				//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
				//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
				//IL_00da: Unknown result type (might be due to invalid IL or missing references)
				//IL_00df: Unknown result type (might be due to invalid IL or missing references)
				//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
				//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
				//IL_0115: Unknown result type (might be due to invalid IL or missing references)
				//IL_011f: Unknown result type (might be due to invalid IL or missing references)
				Main.spriteBatch.Draw(GetGlowTex(), base.Projectile.Center + offsetPercentage.RotatedBy(base.Projectile.rotation - (float)Math.PI / 2f) * TailLength - Main.screenPosition, (Rectangle?)null, Color.SkyBlue, 0f, GetGlowTex().Size() * 0.5f, 0.2f * base.Projectile.scale, (SpriteEffects)0, 0f);
				Main.spriteBatch.Draw(tex, base.Projectile.Center + offsetPercentage.RotatedBy(base.Projectile.rotation - (float)Math.PI / 2f) * TailLength - Main.screenPosition, (Rectangle?)null, Color.White, 0f * Math.Abs(offsetPercentage.X) * 5f * Main.GlobalTimeWrappedHourly, tex.Size() * 0.5f, 0.75f * base.Projectile.scale, (SpriteEffects)0, 0f);
			}
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Voidfrost>(), 60);
	}
}
