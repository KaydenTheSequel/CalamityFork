using System;
using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses;

public class EyeOfCthulhuAI : VanillaAIOverride
{
	private const float ProjectileOffset = 50f;

	public static float Phase1ContactDamageMult = 1.333f;

	public static float Phase2ContactDamageMult = 1.6f;

	public static float Phase3ContactDamageMult = 1.8f;

	public static int BloodShotDamage = 8;

	public override bool AI(Mod mod)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0613: Unknown result type (might be due to invalid IL or missing references)
		//IL_0576: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		//IL_064a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0660: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0692: Unknown result type (might be due to invalid IL or missing references)
		//IL_18f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_18ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c23: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c81: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0802: Unknown result type (might be due to invalid IL or missing references)
		//IL_0807: Unknown result type (might be due to invalid IL or missing references)
		//IL_080f: Unknown result type (might be due to invalid IL or missing references)
		//IL_081b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2128: Unknown result type (might be due to invalid IL or missing references)
		//IL_2133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1407: Unknown result type (might be due to invalid IL or missing references)
		//IL_1450: Unknown result type (might be due to invalid IL or missing references)
		//IL_1456: Unknown result type (might be due to invalid IL or missing references)
		//IL_146a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1474: Unknown result type (might be due to invalid IL or missing references)
		//IL_1479: Unknown result type (might be due to invalid IL or missing references)
		//IL_216e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0844: Unknown result type (might be due to invalid IL or missing references)
		//IL_0849: Unknown result type (might be due to invalid IL or missing references)
		//IL_0851: Unknown result type (might be due to invalid IL or missing references)
		//IL_124b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1256: Unknown result type (might be due to invalid IL or missing references)
		//IL_109d: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_2286: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_21d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_21d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_10bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_10cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_10da: Unknown result type (might be due to invalid IL or missing references)
		//IL_10df: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c91: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_19b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_19c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_19cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_19d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_15cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_15d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_15db: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1100: Unknown result type (might be due to invalid IL or missing references)
		//IL_1137: Unknown result type (might be due to invalid IL or missing references)
		//IL_1150: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ce4: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cfa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d34: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d47: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d51: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d76: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_137d: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_13cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_13fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1280: Unknown result type (might be due to invalid IL or missing references)
		//IL_12af: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1301: Unknown result type (might be due to invalid IL or missing references)
		//IL_1324: Unknown result type (might be due to invalid IL or missing references)
		//IL_1353: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dfc: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d40: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_118f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1196: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c18: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c27: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c33: Unknown result type (might be due to invalid IL or missing references)
		//IL_169f: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ac8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1acd: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_16e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09da: Unknown result type (might be due to invalid IL or missing references)
		//IL_09dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a17: Unknown result type (might be due to invalid IL or missing references)
		//IL_2043: Unknown result type (might be due to invalid IL or missing references)
		//IL_205e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f46: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aeb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a77: Unknown result type (might be due to invalid IL or missing references)
		//IL_207a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2095: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f98: Unknown result type (might be due to invalid IL or missing references)
		//IL_1772: Unknown result type (might be due to invalid IL or missing references)
		//IL_177d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1782: Unknown result type (might be due to invalid IL or missing references)
		//IL_1787: Unknown result type (might be due to invalid IL or missing references)
		//IL_178c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1793: Unknown result type (might be due to invalid IL or missing references)
		//IL_179d: Unknown result type (might be due to invalid IL or missing references)
		//IL_17a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_17aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_17af: Unknown result type (might be due to invalid IL or missing references)
		//IL_17b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b50: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b60: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b66: Unknown result type (might be due to invalid IL or missing references)
		//IL_249c: Unknown result type (might be due to invalid IL or missing references)
		//IL_24a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_24b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_24cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_24e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_24e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_24ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_24f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_24f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_256a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2576: Unknown result type (might be due to invalid IL or missing references)
		//IL_257d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2582: Unknown result type (might be due to invalid IL or missing references)
		//IL_258a: Unknown result type (might be due to invalid IL or missing references)
		//IL_17de: Unknown result type (might be due to invalid IL or missing references)
		//IL_17f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_17fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_17fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1803: Unknown result type (might be due to invalid IL or missing references)
		//IL_1817: Unknown result type (might be due to invalid IL or missing references)
		//IL_181c: Unknown result type (might be due to invalid IL or missing references)
		//IL_181e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1823: Unknown result type (might be due to invalid IL or missing references)
		//IL_182d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1832: Unknown result type (might be due to invalid IL or missing references)
		//IL_1837: Unknown result type (might be due to invalid IL or missing references)
		//IL_254c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2551: Unknown result type (might be due to invalid IL or missing references)
		//IL_2558: Unknown result type (might be due to invalid IL or missing references)
		//IL_255d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2562: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2621: Unknown result type (might be due to invalid IL or missing references)
		//IL_262c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2631: Unknown result type (might be due to invalid IL or missing references)
		//IL_2636: Unknown result type (might be due to invalid IL or missing references)
		//IL_263b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2642: Unknown result type (might be due to invalid IL or missing references)
		//IL_2647: Unknown result type (might be due to invalid IL or missing references)
		//IL_264f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2654: Unknown result type (might be due to invalid IL or missing references)
		//IL_2656: Unknown result type (might be due to invalid IL or missing references)
		//IL_265b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2665: Unknown result type (might be due to invalid IL or missing references)
		//IL_266a: Unknown result type (might be due to invalid IL or missing references)
		//IL_266f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2685: Unknown result type (might be due to invalid IL or missing references)
		//IL_2699: Unknown result type (might be due to invalid IL or missing references)
		//IL_269e: Unknown result type (might be due to invalid IL or missing references)
		//IL_26a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_26b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_26bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_26bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_26c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_26c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_26ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_26d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_26dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_2712: Unknown result type (might be due to invalid IL or missing references)
		//IL_271a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2786: Unknown result type (might be due to invalid IL or missing references)
		//IL_278e: Unknown result type (might be due to invalid IL or missing references)
		//IL_27c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_27de: Unknown result type (might be due to invalid IL or missing references)
		//IL_2725: Unknown result type (might be due to invalid IL or missing references)
		//IL_272c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2739: Unknown result type (might be due to invalid IL or missing references)
		//IL_2749: Unknown result type (might be due to invalid IL or missing references)
		//IL_274f: Unknown result type (might be due to invalid IL or missing references)
		//IL_283e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2849: Unknown result type (might be due to invalid IL or missing references)
		//IL_284e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2853: Unknown result type (might be due to invalid IL or missing references)
		//IL_2858: Unknown result type (might be due to invalid IL or missing references)
		//IL_285f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2864: Unknown result type (might be due to invalid IL or missing references)
		//IL_2955: Unknown result type (might be due to invalid IL or missing references)
		//IL_295c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2961: Unknown result type (might be due to invalid IL or missing references)
		//IL_2877: Unknown result type (might be due to invalid IL or missing references)
		//IL_2894: Unknown result type (might be due to invalid IL or missing references)
		//IL_289a: Unknown result type (might be due to invalid IL or missing references)
		//IL_289c: Unknown result type (might be due to invalid IL or missing references)
		//IL_297e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2996: Unknown result type (might be due to invalid IL or missing references)
		//IL_299c: Unknown result type (might be due to invalid IL or missing references)
		//IL_299e: Unknown result type (might be due to invalid IL or missing references)
		//IL_29a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_29b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_29bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_29be: Unknown result type (might be due to invalid IL or missing references)
		//IL_29c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_29cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_29d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_29d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_28b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_28b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_28d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_28d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_28d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_28dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_28e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_28eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_28f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_28f7: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		Lighting.AddLight(base.NPC.Center, 0.5f, 0.5f, 0.5f);
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		float phase2LifeRatio = (death ? 0.75f : 0.6f);
		float phase3LifeRatio = (death ? 0.4f : 0.3f);
		float finalPhaseRevLifeRatio = (death ? 0.2f : 0.15f);
		float penultimatePhaseDeathLifeRatio = (death ? 0.3f : 0.2f);
		float finalPhaseDeathLifeRatio = (death ? 0.15f : 0.1f);
		bool phase2 = lifeRatio < phase2LifeRatio;
		bool phase3 = lifeRatio < phase3LifeRatio;
		bool finalPhaseRev = lifeRatio < finalPhaseRevLifeRatio;
		bool penultimatePhaseDeath = lifeRatio < penultimatePhaseDeathLifeRatio;
		bool finalPhaseDeath = lifeRatio < finalPhaseDeathLifeRatio;
		float lineUpDist = (death ? 15f : 20f);
		base.NPC.damage = (int)Math.Round((float)base.NPC.defDamage * (phase3 ? Phase3ContactDamageMult : (phase2 ? Phase2ContactDamageMult : Phase1ContactDamageMult)));
		float servantAndProjectileVelocity = (death ? 7f : 6f);
		base.NPC.reflectsProjectiles = false;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
		}
		bool dead = Main.player[base.NPC.target].dead;
		float eyeRotation = (float)Math.Atan2(x: base.NPC.Center.X - Main.player[base.NPC.target].position.X - (float)(Main.player[base.NPC.target].width / 2), y: base.NPC.position.Y + (float)base.NPC.height - 59f - Main.player[base.NPC.target].position.Y - (float)(Main.player[base.NPC.target].height / 2)) + (float)Math.PI / 2f;
		if (eyeRotation < 0f)
		{
			eyeRotation += (float)Math.PI * 2f;
		}
		else if (eyeRotation > (float)Math.PI * 2f)
		{
			eyeRotation -= (float)Math.PI * 2f;
		}
		float eyeRotationAcceleration = 0f;
		if (base.NPC.ai[0] == 0f && base.NPC.ai[1] == 0f)
		{
			eyeRotationAcceleration = 0.04f;
		}
		if (base.NPC.ai[0] == 0f && base.NPC.ai[1] == 2f && base.NPC.ai[2] > 40f)
		{
			eyeRotationAcceleration = 0.1f;
		}
		if (base.NPC.ai[0] == 3f && base.NPC.ai[1] == 0f)
		{
			eyeRotationAcceleration = 0.1f;
		}
		if (base.NPC.ai[0] == 3f && base.NPC.ai[1] == 2f && base.NPC.ai[2] > 40f)
		{
			eyeRotationAcceleration = 0.16f;
		}
		if (base.NPC.ai[0] == 3f && base.NPC.ai[1] == 4f && base.NPC.ai[2] > lineUpDist)
		{
			eyeRotationAcceleration = 0.3f;
		}
		if (base.NPC.ai[0] == 3f && base.NPC.ai[1] == 5f)
		{
			eyeRotationAcceleration = 0.1f;
		}
		if (base.NPC.rotation < eyeRotation)
		{
			if (eyeRotation - base.NPC.rotation > (float)Math.PI)
			{
				base.NPC.rotation -= eyeRotationAcceleration;
			}
			else
			{
				base.NPC.rotation += eyeRotationAcceleration;
			}
		}
		else if (base.NPC.rotation > eyeRotation)
		{
			if (base.NPC.rotation - eyeRotation > (float)Math.PI)
			{
				base.NPC.rotation += eyeRotationAcceleration;
			}
			else
			{
				base.NPC.rotation -= eyeRotationAcceleration;
			}
		}
		if (base.NPC.rotation > eyeRotation - eyeRotationAcceleration && base.NPC.rotation < eyeRotation + eyeRotationAcceleration)
		{
			base.NPC.rotation = eyeRotation;
		}
		if (base.NPC.rotation < 0f)
		{
			base.NPC.rotation += (float)Math.PI * 2f;
		}
		else if (base.NPC.rotation > (float)Math.PI * 2f)
		{
			base.NPC.rotation -= (float)Math.PI * 2f;
		}
		if (base.NPC.rotation > eyeRotation - eyeRotationAcceleration && base.NPC.rotation < eyeRotation + eyeRotationAcceleration)
		{
			base.NPC.rotation = eyeRotation;
		}
		if (Main.rand.NextBool(5))
		{
			int randomBlood = Dust.NewDust(new Vector2(base.NPC.position.X, base.NPC.position.Y + (float)base.NPC.height * 0.25f), base.NPC.width, (int)((float)base.NPC.height * 0.5f), 5, base.NPC.velocity.X, 2f);
			Dust obj = Main.dust[randomBlood];
			obj.velocity.X *= 0.5f;
			obj.velocity.Y *= 0.1f;
		}
		bool shootProjectile = Collision.CanHitLine(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1) && base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center).AngleBetween((base.NPC.rotation + (float)Math.PI / 2f).ToRotationVector2()) < MathHelper.ToRadians(18f) && Vector2.Distance(base.NPC.Center, Main.player[base.NPC.target].Center) > 240f;
		bool charge = Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) >= 320f;
		if ((dead || Main.IsItDay()) && !BossRushEvent.BossRushActive)
		{
			base.NPC.velocity.Y -= 0.04f;
			if (base.NPC.timeLeft > 10)
			{
				base.NPC.timeLeft = 10;
			}
			return false;
		}
		if (base.NPC.ai[0] == 0f)
		{
			if (base.NPC.ai[1] == 0f)
			{
				float hoverSpeed = (death ? (7f + 3f * (1f - lifeRatio)) : 7f);
				float hoverAcceleration = (death ? (0.15f + 0.1f * (1f - lifeRatio)) : 0.15f);
				if (Main.getGoodWorld)
				{
					hoverSpeed += 3f;
					hoverAcceleration += 0.08f;
				}
				float attackSwitchTimer = (death ? (120f - 180f * (1f - lifeRatio)) : 180f);
				bool timeToCharge = base.NPC.ai[2] >= attackSwitchTimer;
				Vector2 hoverDestination = Main.player[base.NPC.target].Center - Vector2.UnitY * 400f;
				Vector2 idealVelocity = base.NPC.SafeDirectionTo(hoverDestination) * (hoverSpeed + (timeToCharge ? ((base.NPC.ai[2] - attackSwitchTimer) * 0.01f) : 0f));
				base.NPC.SimpleFlyMovement(idealVelocity, hoverAcceleration + (timeToCharge ? ((base.NPC.ai[2] - attackSwitchTimer) * 0.001f) : 0f));
				base.NPC.ai[2]++;
				if (timeToCharge & charge)
				{
					base.NPC.ai[1] = 1f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
					base.NPC.netUpdate = true;
				}
				else if (base.NPC.WithinRange(hoverDestination, 900f))
				{
					if (!Main.player[base.NPC.target].dead)
					{
						base.NPC.ai[3]++;
					}
					float servantSpawnGateValue = (death ? 15f : 40f);
					if (Main.getGoodWorld)
					{
						servantSpawnGateValue *= 0.8f;
					}
					if ((base.NPC.ai[3] >= servantSpawnGateValue) & shootProjectile)
					{
						base.NPC.ai[3] = 0f;
						base.NPC.rotation = eyeRotation;
						Vector2 servantSpawnVelocity = base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center) * servantAndProjectileVelocity;
						Vector2 servantSpawnCenter = base.NPC.Center + servantSpawnVelocity.SafeNormalize(Vector2.UnitY) * 50f;
						int maxServants = 3;
						bool spawnServant = NPC.CountNPCS(5) < maxServants;
						if (spawnServant)
						{
							SoundEngine.PlaySound(in SoundID.NPCHit1, servantSpawnCenter);
						}
						if (Main.netMode != 1)
						{
							if (spawnServant)
							{
								int eye = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)servantSpawnCenter.X, (int)servantSpawnCenter.Y, 5);
								Main.npc[eye].velocity = servantSpawnVelocity;
								if (Main.dedServ && eye < Main.maxNPCs)
								{
									NetMessage.SendData(23, -1, -1, null, eye);
								}
							}
							else
							{
								int projType = 814;
								int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + servantSpawnVelocity.SafeNormalize(Vector2.UnitY) * 50f, servantSpawnVelocity * 2f, projType, BloodShotDamage, 0f, Main.myPlayer);
								Main.projectile[proj].timeLeft = 600;
							}
						}
						if (spawnServant)
						{
							for (int m = 0; m < 10; m++)
							{
								Dust.NewDust(servantSpawnCenter, 20, 20, 5, servantSpawnVelocity.X * 0.4f, servantSpawnVelocity.Y * 0.4f);
							}
						}
					}
				}
			}
			else if (base.NPC.ai[1] == 1f)
			{
				base.NPC.rotation = eyeRotation;
				float additionalVelocityPerCharge = 2f;
				float chargeSpeed = (death ? 10.5f : 8f) + base.NPC.ai[3] * additionalVelocityPerCharge;
				if (death)
				{
					chargeSpeed += 10f * (1f - lifeRatio);
				}
				if (Main.getGoodWorld)
				{
					chargeSpeed += 4f;
				}
				base.NPC.velocity = base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center) * chargeSpeed;
				base.NPC.ai[1] = 2f;
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
			}
			else if (base.NPC.ai[1] == 2f)
			{
				int chargeDelay = (death ? (75 - (int)Math.Round(30f * (1f - lifeRatio))) : 95);
				if (Main.getGoodWorld)
				{
					chargeDelay -= 30;
				}
				float slowDownGateValue = (float)chargeDelay * (death ? 0.85f : 0.65f);
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] >= slowDownGateValue)
				{
					float decelerationScalar = (death ? ((lifeRatio - phase2LifeRatio) / (1f - phase2LifeRatio)) : 1f);
					if (decelerationScalar < 0f)
					{
						decelerationScalar = 0f;
					}
					NPC nPC = base.NPC;
					nPC.velocity *= MathHelper.Lerp(death ? 0.76f : 0.92f, death ? 0.88f : 0.96f, decelerationScalar);
					if (Main.getGoodWorld)
					{
						NPC nPC2 = base.NPC;
						nPC2.velocity *= 0.99f;
					}
					if ((double)base.NPC.velocity.X > -0.1 && (double)base.NPC.velocity.X < 0.1)
					{
						base.NPC.velocity.X = 0f;
					}
					if ((double)base.NPC.velocity.Y > -0.1 && (double)base.NPC.velocity.Y < 0.1)
					{
						base.NPC.velocity.Y = 0f;
					}
				}
				else
				{
					base.NPC.rotation = base.NPC.velocity.ToRotation() - (float)Math.PI / 2f;
				}
				if (base.NPC.ai[2] >= (float)chargeDelay)
				{
					base.NPC.ai[3]++;
					base.NPC.ai[2] = 0f;
					base.NPC.rotation = eyeRotation;
					float numCharges = (death ? 4f : 3f);
					if (base.NPC.ai[3] >= numCharges)
					{
						base.NPC.ai[1] = 0f;
						base.NPC.ai[3] = 0f;
					}
					else
					{
						base.NPC.ai[1] = 1f;
					}
				}
			}
			if (phase2)
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
			}
		}
		else if (base.NPC.ai[0] == 1f || base.NPC.ai[0] == 2f)
		{
			if (Main.getGoodWorld)
			{
				base.NPC.reflectsProjectiles = true;
			}
			if (base.NPC.ai[0] == 1f)
			{
				base.NPC.ai[2] += 0.005f;
				if (base.NPC.ai[2] > 0.5f)
				{
					base.NPC.ai[2] = 0.5f;
				}
			}
			else
			{
				base.NPC.ai[2] -= 0.005f;
				if (base.NPC.ai[2] < 0f)
				{
					base.NPC.ai[2] = 0f;
				}
			}
			base.NPC.rotation += base.NPC.ai[2];
			float phaseChangeRate = (death ? 2f : 1f);
			float servantSpawnGateValue2 = (Main.getGoodWorld ? 4f : 20f);
			base.NPC.ai[1] += phaseChangeRate;
			if (base.NPC.ai[1] % servantSpawnGateValue2 == 0f)
			{
				float servantVelocity = (death ? 9.3f : 5.65f);
				Vector2 servantSpawnVelocity2 = Main.rand.NextVector2CircularEdge(servantVelocity, servantVelocity);
				if (Main.getGoodWorld)
				{
					servantSpawnVelocity2 *= 3f;
				}
				Vector2 servantSpawnCenter2 = base.NPC.Center + servantSpawnVelocity2.SafeNormalize(Vector2.UnitY) * 50f;
				if (Main.netMode != 1)
				{
					int servantSpawn = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)servantSpawnCenter2.X, (int)servantSpawnCenter2.Y, 5);
					Main.npc[servantSpawn].velocity.X = servantSpawnVelocity2.X;
					Main.npc[servantSpawn].velocity.Y = servantSpawnVelocity2.Y;
					if (Main.dedServ && servantSpawn < Main.maxNPCs)
					{
						NetMessage.SendData(23, -1, -1, null, servantSpawn);
					}
				}
				for (int n = 0; n < 10; n++)
				{
					Dust.NewDust(servantSpawnCenter2, 20, 20, 5, servantSpawnVelocity2.X * 0.4f, servantSpawnVelocity2.Y * 0.4f);
				}
			}
			if (base.NPC.ai[1] == 100f)
			{
				base.NPC.ai[0]++;
				base.NPC.ai[1] = 0f;
				if (base.NPC.ai[0] == 3f)
				{
					base.NPC.ai[2] = 0f;
				}
				else
				{
					SoundEngine.PlaySound(in SoundID.NPCHit1, base.NPC.Center);
					if (!Main.dedServ)
					{
						for (int phase2Gore = 0; phase2Gore < 2; phase2Gore++)
						{
							Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 8);
							Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 7);
							Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 6);
						}
					}
					for (int i = 0; i < 20; i++)
					{
						Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f);
					}
					SoundEngine.PlaySound(in SoundID.Roar, base.NPC.Center);
				}
			}
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f);
			NPC nPC3 = base.NPC;
			nPC3.velocity *= 0.98f;
			if ((double)base.NPC.velocity.X > -0.1 && (double)base.NPC.velocity.X < 0.1)
			{
				base.NPC.velocity.X = 0f;
			}
			if ((double)base.NPC.velocity.Y > -0.1 && (double)base.NPC.velocity.Y < 0.1)
			{
				base.NPC.velocity.Y = 0f;
			}
		}
		else
		{
			base.NPC.defense = 0;
			if ((base.NPC.ai[1] == 0f) & phase3)
			{
				base.NPC.ai[1] = 5f;
			}
			if (base.NPC.ai[1] == 0f)
			{
				float hoverSpeed2 = (death ? 7.5f : 5.5f) + (death ? 8.5f : 3f) * (phase2LifeRatio - lifeRatio);
				float hoverAcceleration2 = (death ? 0.08f : 0.06f) + (death ? 0.08f : 0.02f) * (phase2LifeRatio - lifeRatio);
				Vector2 hoverDestination2 = Main.player[base.NPC.target].Center - Vector2.UnitY * 400f;
				float distanceFromHoverDestination = base.NPC.Distance(hoverDestination2);
				if (distanceFromHoverDestination > 400f)
				{
					hoverSpeed2 += 1.25f;
					hoverAcceleration2 += 0.075f;
					if (distanceFromHoverDestination > 600f)
					{
						hoverSpeed2 += 1.25f;
						hoverAcceleration2 += 0.075f;
						if (distanceFromHoverDestination > 800f)
						{
							hoverSpeed2 += 1.25f;
							hoverAcceleration2 += 0.075f;
						}
					}
				}
				if (Main.getGoodWorld)
				{
					hoverSpeed2++;
					hoverAcceleration2 += 0.1f;
				}
				float phaseLimit = (death ? (160f - 150f * (phase2LifeRatio - lifeRatio)) : 200f);
				bool timeToCharge2 = base.NPC.ai[2] >= phaseLimit;
				Vector2 idealHoverVelocity = base.NPC.SafeDirectionTo(hoverDestination2) * (hoverSpeed2 + (timeToCharge2 ? ((base.NPC.ai[2] - phaseLimit) * 0.01f) : 0f));
				base.NPC.SimpleFlyMovement(idealHoverVelocity, hoverAcceleration2 + (timeToCharge2 ? ((base.NPC.ai[2] - phaseLimit) * 0.001f) : 0f));
				base.NPC.ai[2]++;
				if (death)
				{
					float projectileGateValue = ((lifeRatio < 0.5f) ? 40f : 60f);
					if ((base.NPC.ai[2] % projectileGateValue == 0f) & shootProjectile)
					{
						Vector2 projectileVelocity = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * servantAndProjectileVelocity * 2f;
						_ = base.NPC.Center + projectileVelocity;
						if (Main.netMode != 1)
						{
							int type = 814;
							int numProj = 3;
							float rotation = MathHelper.ToRadians(18f);
							for (int j = 0; j < numProj; j++)
							{
								Vector2 perturbedSpeed = projectileVelocity.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)j / (float)(numProj - 1)));
								int proj2 = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + perturbedSpeed.SafeNormalize(Vector2.UnitY) * 50f, perturbedSpeed, type, BloodShotDamage, 0f, Main.myPlayer);
								Main.projectile[proj2].timeLeft = 600;
							}
						}
					}
				}
				if (timeToCharge2 & charge)
				{
					base.NPC.ai[1] = 1f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[1] == 1f)
			{
				SoundEngine.PlaySound(in SoundID.ForceRoar, base.NPC.Center);
				base.NPC.rotation = eyeRotation;
				float additionalVelocityPerCharge2 = 3f;
				float chargeSpeed2 = (death ? 12f : 10f) + (death ? 10f : 3.5f) * (phase2LifeRatio - lifeRatio) + base.NPC.ai[3] * additionalVelocityPerCharge2;
				if (base.NPC.ai[3] == 1f)
				{
					chargeSpeed2 *= 1.15f;
				}
				if (base.NPC.ai[3] == 2f)
				{
					chargeSpeed2 *= 1.3f;
				}
				if (Main.getGoodWorld)
				{
					chargeSpeed2 *= 1.2f;
				}
				base.NPC.velocity = base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center) * chargeSpeed2;
				base.NPC.ai[1] = 2f;
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
			}
			else if (base.NPC.ai[1] == 2f)
			{
				int phase2ChargeDelay = (death ? (70 - (int)Math.Round(25f * (phase2LifeRatio - lifeRatio))) : 85);
				float slowDownGateValue2 = (float)phase2ChargeDelay * (death ? 0.9f : 0.75f);
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] >= slowDownGateValue2)
				{
					float decelerationScalar2 = (death ? ((lifeRatio - phase3LifeRatio) / (phase2LifeRatio - phase3LifeRatio)) : 1f);
					if (decelerationScalar2 < 0f)
					{
						decelerationScalar2 = 0f;
					}
					NPC nPC4 = base.NPC;
					nPC4.velocity *= MathHelper.Lerp(death ? 0.6f : 0.9f, death ? 0.7f : 0.95f, decelerationScalar2);
					if ((double)base.NPC.velocity.X > -0.1 && (double)base.NPC.velocity.X < 0.1)
					{
						base.NPC.velocity.X = 0f;
					}
					if ((double)base.NPC.velocity.Y > -0.1 && (double)base.NPC.velocity.Y < 0.1)
					{
						base.NPC.velocity.Y = 0f;
					}
				}
				else
				{
					base.NPC.rotation = base.NPC.velocity.ToRotation() - (float)Math.PI / 2f;
				}
				if (base.NPC.ai[2] >= (float)phase2ChargeDelay)
				{
					base.NPC.ai[3]++;
					base.NPC.ai[2] = 0f;
					base.NPC.rotation = eyeRotation;
					float numCharges2 = (death ? 4f : 3f);
					if (base.NPC.ai[3] >= numCharges2)
					{
						base.NPC.ai[1] = 0f;
						base.NPC.ai[3] = 0f;
						base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
					}
					else
					{
						base.NPC.ai[1] = 1f;
					}
				}
			}
			else if (base.NPC.ai[1] == 3f)
			{
				if (((base.NPC.ai[3] == 4f) & phase3) && base.NPC.Center.Y > Main.player[base.NPC.target].Center.Y)
				{
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				}
				else if (Main.netMode != 1)
				{
					float speedBoost = (death ? (10f * (phase3LifeRatio - lifeRatio)) : (7f * (phase3LifeRatio - lifeRatio)));
					float finalChargeSpeed = (death ? 23f : 18f) + speedBoost;
					Vector2 eyeChargeDirection = base.NPC.Center;
					float targetX = Main.player[base.NPC.target].Center.X - eyeChargeDirection.X;
					float targetY = Main.player[base.NPC.target].Center.Y - eyeChargeDirection.Y;
					float targetVelocity = Math.Abs(Main.player[base.NPC.target].velocity.X) + Math.Abs(Main.player[base.NPC.target].velocity.Y) / 4f;
					targetVelocity += 10f - targetVelocity;
					if (targetVelocity < (death ? 2f : 5f))
					{
						targetVelocity = (death ? 2f : 5f);
					}
					if (targetVelocity > (death ? 6f : 15f))
					{
						targetVelocity = (death ? 6f : 15f);
					}
					if (base.NPC.ai[2] == -1f)
					{
						targetVelocity *= 4f;
						finalChargeSpeed *= 1.3f;
					}
					targetX -= Main.player[base.NPC.target].velocity.X * targetVelocity;
					targetY -= Main.player[base.NPC.target].velocity.Y * targetVelocity / 4f;
					float targetDistance = (float)Math.Sqrt(targetX * targetX + targetY * targetY);
					float num = targetDistance;
					targetDistance = finalChargeSpeed / targetDistance;
					base.NPC.velocity.X = targetX * targetDistance;
					base.NPC.velocity.Y = targetY * targetDistance;
					if (num < 100f)
					{
						if (Math.Abs(base.NPC.velocity.X) > Math.Abs(base.NPC.velocity.Y))
						{
							float absoluteXVel = Math.Abs(base.NPC.velocity.X);
							float absoluteYVel = Math.Abs(base.NPC.velocity.Y);
							if (base.NPC.Center.X > Main.player[base.NPC.target].Center.X)
							{
								absoluteYVel *= -1f;
							}
							if (base.NPC.Center.Y > Main.player[base.NPC.target].Center.Y)
							{
								absoluteXVel *= -1f;
							}
							base.NPC.velocity.X = absoluteYVel;
							base.NPC.velocity.Y = absoluteXVel;
						}
					}
					else if (Math.Abs(base.NPC.velocity.X) > Math.Abs(base.NPC.velocity.Y))
					{
						float absoluteEyeVel = (Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) / 2f;
						float absoluteEyeVelBackup = absoluteEyeVel;
						if (base.NPC.Center.X > Main.player[base.NPC.target].Center.X)
						{
							absoluteEyeVelBackup *= -1f;
						}
						if (base.NPC.Center.Y > Main.player[base.NPC.target].Center.Y)
						{
							absoluteEyeVel *= -1f;
						}
						base.NPC.velocity.X = absoluteEyeVelBackup;
						base.NPC.velocity.Y = absoluteEyeVel;
					}
					base.NPC.ai[1] = 4f;
					base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				}
			}
			else if (base.NPC.ai[1] == 4f)
			{
				if (base.NPC.ai[2] == 0f)
				{
					SoundEngine.PlaySound(in SoundID.ForceRoarPitched, base.NPC.Center);
				}
				float lineUpDistControl = lineUpDist;
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] == lineUpDistControl && Vector2.Distance(base.NPC.position, Main.player[base.NPC.target].position) < 200f)
				{
					base.NPC.ai[2]--;
				}
				if (base.NPC.ai[2] >= lineUpDistControl)
				{
					NPC nPC5 = base.NPC;
					nPC5.velocity *= 0.95f;
					if ((double)base.NPC.velocity.X > -0.1 && (double)base.NPC.velocity.X < 0.1)
					{
						base.NPC.velocity.X = 0f;
					}
					if ((double)base.NPC.velocity.Y > -0.1 && (double)base.NPC.velocity.Y < 0.1)
					{
						base.NPC.velocity.Y = 0f;
					}
				}
				else
				{
					base.NPC.rotation = base.NPC.velocity.ToRotation() - (float)Math.PI / 2f;
				}
				float lineUpDistNetUpdate = lineUpDistControl + 13f;
				if (base.NPC.ai[2] >= lineUpDistNetUpdate)
				{
					base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
					base.NPC.ai[3]++;
					base.NPC.ai[2] = 0f;
					float maxCharges = ((!death) ? (finalPhaseRev ? 2f : 3f) : (finalPhaseDeath ? 0f : (penultimatePhaseDeath ? 1f : 2f)));
					if (base.NPC.ai[3] >= maxCharges)
					{
						base.NPC.ai[1] = 0f;
						base.NPC.ai[3] = 0f;
					}
					else
					{
						base.NPC.ai[1] = 3f;
					}
				}
			}
			else if (base.NPC.ai[1] == 5f)
			{
				float offset = (death ? 540f : 600f);
				float speedBoost2 = (death ? (15f * (phase3LifeRatio - lifeRatio)) : (5f * (phase3LifeRatio - lifeRatio)));
				float accelerationBoost = (death ? (0.425f * (phase3LifeRatio - lifeRatio)) : (0.125f * (phase3LifeRatio - lifeRatio)));
				float hoverSpeed3 = (death ? 10f : 8f) + speedBoost2;
				float hoverAcceleration3 = (death ? 0.3125f : 0.25f) + accelerationBoost;
				bool horizontalCharge = calamityGlobalNPC.newAI[0] == 1f || calamityGlobalNPC.newAI[0] == 3f;
				float timeGateValue = (horizontalCharge ? (110f - (death ? (30f * (phase3LifeRatio - lifeRatio)) : 0f)) : (95f - (death ? (55f * (phase3LifeRatio - lifeRatio)) : 0f)));
				if (base.NPC.ai[2] > timeGateValue)
				{
					float velocityScalar = base.NPC.ai[2] - timeGateValue;
					hoverSpeed3 += velocityScalar * 0.05f;
					hoverAcceleration3 += velocityScalar * 0.0025f;
				}
				Vector2 eyeLineUpChargeDirection = base.NPC.Center;
				_ = Main.player[base.NPC.target].Center;
				_ = Main.player[base.NPC.target].Center;
				Vector2 hoverDestination3 = Main.player[base.NPC.target].Center + Vector2.UnitY * offset;
				if (horizontalCharge)
				{
					float horizontalChargeOffset = (death ? 450f : 500f);
					offset = ((calamityGlobalNPC.newAI[0] == 1f) ? (0f - horizontalChargeOffset) : horizontalChargeOffset);
					hoverSpeed3 *= 1.5f;
					hoverAcceleration3 *= 1.5f;
					hoverDestination3 = Main.player[base.NPC.target].Center + Vector2.UnitX * offset;
				}
				Vector2 idealHoverVelocity2 = base.NPC.SafeDirectionTo(hoverDestination3) * hoverSpeed3;
				base.NPC.SimpleFlyMovement(idealHoverVelocity2, hoverAcceleration3);
				float servantSpawnGateValue3 = ((!horizontalCharge) ? (death ? 17f : 27f) : (death ? 23f : 35f));
				float maxServantSpawnsPerAttack = 2f;
				base.NPC.ai[2]++;
				if (((base.NPC.ai[2] % servantSpawnGateValue3 == 0f) & shootProjectile) && base.NPC.ai[2] <= servantSpawnGateValue3 * maxServantSpawnsPerAttack)
				{
					Vector2 servantSpawnVelocity3 = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * servantAndProjectileVelocity;
					Vector2 servantSpawnCenter3 = base.NPC.Center + servantSpawnVelocity3.SafeNormalize(Vector2.UnitY) * 50f;
					if (death)
					{
						servantSpawnCenter3 = Main.player[base.NPC.target].Center + Main.rand.NextVector2CircularEdge(1600f, 1600f);
						servantSpawnVelocity3 = (Main.player[base.NPC.target].Center - servantSpawnCenter3).SafeNormalize(Vector2.UnitY) * servantAndProjectileVelocity * 2f;
					}
					int maxServants2 = ((!death) ? (finalPhaseRev ? 2 : 4) : (finalPhaseDeath ? 2 : (penultimatePhaseDeath ? 3 : 4)));
					bool spawnServant2 = NPC.CountNPCS(5) < maxServants2;
					if (spawnServant2)
					{
						SoundEngine.PlaySound(in SoundID.NPCDeath13, servantSpawnCenter3);
						for (int k = 0; k < 10; k++)
						{
							Dust.NewDust(servantSpawnCenter3, 20, 20, 5, servantSpawnVelocity3.X * 0.4f, servantSpawnVelocity3.Y * 0.4f);
						}
					}
					if (Main.netMode != 1)
					{
						if (spawnServant2)
						{
							int eye2 = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)servantSpawnCenter3.X, (int)servantSpawnCenter3.Y, 5);
							Main.npc[eye2].velocity.X = servantSpawnVelocity3.X;
							Main.npc[eye2].velocity.Y = servantSpawnVelocity3.Y;
							if (Main.dedServ && eye2 < Main.maxNPCs)
							{
								NetMessage.SendData(23, -1, -1, null, eye2);
							}
						}
						else if (!Main.getGoodWorld)
						{
							Vector2 spawnVelocity = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * servantAndProjectileVelocity;
							int amount = ((!death) ? 1 : 3);
							for (int l = 0; l < amount; l++)
							{
								Vector2 perturbedSpeed2 = spawnVelocity.RotatedBy(MathHelper.Lerp(-0.5f, 0.5f, (float)l / (float)(amount - 1))) * (death ? 1.25f : 1f);
								int projType2 = 814;
								int proj3 = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + perturbedSpeed2.SafeNormalize(Vector2.UnitY) * 50f, perturbedSpeed2 * 2f, projType2, BloodShotDamage, 0f, Main.myPlayer);
								Main.projectile[proj3].timeLeft = 600;
							}
						}
						if (Main.getGoodWorld)
						{
							int type2 = 814;
							Vector2 projectileVelocity2 = servantSpawnVelocity3 * 3f;
							int numProj2 = (death ? 5 : 3);
							float rotation2 = MathHelper.ToRadians(20f);
							for (int num2 = 0; num2 < numProj2; num2++)
							{
								Vector2 perturbedSpeed3 = projectileVelocity2.RotatedBy(MathHelper.Lerp(0f - rotation2, rotation2, (float)num2 / (float)(numProj2 - 1)));
								int proj4 = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + perturbedSpeed3.SafeNormalize(Vector2.UnitY) * 50f, perturbedSpeed3, type2, 15, 0f, Main.myPlayer);
								Main.projectile[proj4].timeLeft = 600;
							}
						}
					}
				}
				float requiredDistanceForHorizontalCharge = 160f;
				if (base.NPC.ai[2] >= timeGateValue && (base.NPC.Distance(hoverDestination3) < requiredDistanceForHorizontalCharge || !horizontalCharge))
				{
					switch ((int)calamityGlobalNPC.newAI[0])
					{
					case 0:
						base.NPC.ai[1] = 3f;
						base.NPC.ai[2] = -1f;
						base.NPC.ai[3] = -1f;
						break;
					case 1:
						base.NPC.ai[1] = 6f;
						base.NPC.ai[2] = 0f;
						break;
					case 2:
						base.NPC.ai[1] = 3f;
						base.NPC.ai[2] = -1f;
						break;
					case 3:
						base.NPC.ai[1] = 6f;
						base.NPC.ai[2] = 0f;
						break;
					}
					calamityGlobalNPC.newAI[0] += ((death && calamityGlobalNPC.newAI[0] % 2f != 0f) ? ((float)Main.rand.Next(2) + 1f) : 1f);
					if (calamityGlobalNPC.newAI[0] > 3f)
					{
						calamityGlobalNPC.newAI[0] = (death ? ((float)Main.rand.Next(2)) : 0f);
					}
					base.NPC.SyncExtraAI();
				}
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
			}
			else if (base.NPC.ai[1] == 6f)
			{
				if (Main.netMode != 1)
				{
					float speedBoost3 = (death ? (15f * (phase3LifeRatio - lifeRatio)) : (5f * (phase3LifeRatio - lifeRatio)));
					float chargeSpeed3 = (death ? 23f : 18f) + speedBoost3;
					base.NPC.velocity = base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center) * chargeSpeed3;
					base.NPC.ai[1] = 7f;
					base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				}
			}
			else if (base.NPC.ai[1] == 7f)
			{
				if (base.NPC.ai[2] == 0f)
				{
					SoundEngine.PlaySound(in SoundID.ForceRoar, base.NPC.Center);
				}
				float lineUpDistControl2 = (float)Math.Round(lineUpDist * 2.5f);
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] == lineUpDistControl2 && Vector2.Distance(base.NPC.position, Main.player[base.NPC.target].position) < 200f)
				{
					base.NPC.ai[2]--;
				}
				if (base.NPC.ai[2] >= lineUpDistControl2)
				{
					NPC nPC6 = base.NPC;
					nPC6.velocity *= 0.95f;
					if ((double)base.NPC.velocity.X > -0.1 && (double)base.NPC.velocity.X < 0.1)
					{
						base.NPC.velocity.X = 0f;
					}
					if ((double)base.NPC.velocity.Y > -0.1 && (double)base.NPC.velocity.Y < 0.1)
					{
						base.NPC.velocity.Y = 0f;
					}
				}
				else
				{
					base.NPC.rotation = base.NPC.velocity.ToRotation() - (float)Math.PI / 2f;
				}
				float lineUpDistNetUpdate2 = lineUpDistControl2 + 13f;
				if (base.NPC.ai[2] >= lineUpDistNetUpdate2)
				{
					base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
					base.NPC.ai[2] = 0f;
					base.NPC.ai[1] = 0f;
				}
			}
		}
		return false;
	}
}
