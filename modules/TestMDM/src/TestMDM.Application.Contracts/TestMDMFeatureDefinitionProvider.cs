using Volo.Abp.Features;
using Volo.Abp.Localization;
using Volo.Abp.Validation.StringValues;
using TetsMDM;
using TestMDM.Localization;

namespace FeaturesDemo
{
    public class TestMDMFeatureDefinitionProvider : FeatureDefinitionProvider
    {
        public override void Define(IFeatureDefinitionContext context)
        {
            var masterGroup = context.AddGroup(TestMDMFeatures.GroupName);

            var enable = masterGroup.AddFeature(
                TestMDMFeatures.Enable,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<TestMDMResource>("EnableMDM"),
                valueType: new ToggleStringValueType()
            );

            enable.CreateChild(
               TestMDMFeatures.Distributor,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<TestMDMResource>("EnableDistributor"),
                valueType: new ToggleStringValueType()
            );
        }
    }
}