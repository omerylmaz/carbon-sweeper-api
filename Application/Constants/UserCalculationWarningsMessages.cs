public static class UserCalculationWarningsEncouragementMessages
{
    public static string UserElectricityBad(decimal value)
    {
        return $"Your electricity footprint value {value/1000} ton is too high. Consider reducing your energy consumption to make a positive impact on the environment.";
    }

    public static string UserElectricityPerfect(decimal value)
    {
        return $"Your electricity footprint value {value/1000} ton is within an acceptable range. Keep up the good work and continue to conserve energy.";
    }

    public static string UserCoalBad(decimal value)
    {
        return $"Your coal footprint value {value/1000} ton is significant. To protect our environment, it is crucial to transition away from coal. Explore greener alternatives.";
    }

    public static string UserCoalPerfect(decimal value)
    {
        return $"Your coal footprint value {value/1000} ton is commendable. By not using coal, you're contributing to a cleaner future. Well done!";
    }

    public static string UserLpgBad(decimal value)
    {
        return $"Your natural gas footprint value {value/1000} ton is high. Consider reducing your natural gas usage and explore energy-efficient alternatives.";
    }

    public static string UserLPGPerfect(decimal value)
    {
        return $"Your natural gas footprint value {value/1000} ton is within a reasonable range. Keep up the good work and continue to minimize your carbon emissions.";
    }

    public static string UserCarFuelBad(decimal value)
    {
        return $"Your car fuel footprint value {value/1000} ton is too high. To reduce your carbon footprint, consider using alternative transportation methods or carpooling.";
    }

    public static string UserCarFuelPerfect(decimal value)
    {
        return $"Your car fuel footprint value {value/1000} ton is acceptable. Keep choosing fuel-efficient vehicles and optimizing your driving habits.";
    }

    public static string UserPublicTransportPerfect(decimal value)
    {
        return $"Your public transport footprint value {value/1000} ton is fantastic. Using public transport is an excellent way to reduce emissions. Well done!";
    }

    public static string UserPaperProductBad(decimal value)
    {
        return $"Your paper product footprint value {value/1000} ton is significant. Explore ways to minimize paper waste and opt for recycled or sustainable alternatives.";
    }

    public static string UserPaperProductPerfect(decimal value)
    {
        return $"Your paper product footprint value {value/1000} ton is within an acceptable range. Continue to make conscious choices to reduce paper consumption.";
    }

    public static string UserDressingBad(decimal value)
    {
        return $"Your clothing footprint value {value/1000} ton is high. Consider donating or recycling clothes and choose sustainable fashion options to minimize waste.";
    }

    public static string UserDressingPerfect(decimal value)
    {
        return $"Your clothing footprint value {value/1000} ton is within a reasonable range. Keep up the good work in making eco-friendly fashion choices.";
    }

    public static string UserElectronicsBad(decimal value)
    {
        return $"Your electronics footprint value {value/1000} ton is significant. To reduce e-waste, consider recycling old devices and purchasing energy-efficient electronics.";
    }

    public static string UserElectronicsPerfect(decimal value)
    {
        return $"Your electronics footprint value {value/1000} ton is acceptable. Keep choosing energy-efficient electronics and responsibly disposing of old devices.";
    }

    public static string UserFoodBad(decimal value)
    {
        return $"Your food footprint value {value/1000} ton is high. Consider adopting sustainable eating habits such as reducing meat consumption and supporting local, organic food.";
    }

    public static string UserFoodPerfect(decimal value)
    {
        return $"Your food footprint value {value/1000} ton is within a reasonable range. Keep making conscious choices to minimize the environmental impact of your diet.";
    }

    public static string UserFunBad(decimal value)
    {
        return $"Your recreational footprint value {value/1000} ton is high. Explore eco-friendly activities and hobbies to reduce your carbon footprint while still having fun.";
    }

    public static string UserFunPerfect(decimal value)
    {
        return $"Your recreational footprint value {value/1000} ton is within an acceptable range. Continue to enjoy eco-friendly activities and inspire others to do the same.";
    }
    public static string UserReductionPerfect()
    {
        return $"You have reduced your carbon footprint. Good job to you";
    }
    public static string UserReductionBad()
    {
        return $"You have increased your carbon footprint. We hope you will decrease it.";
    }
}
