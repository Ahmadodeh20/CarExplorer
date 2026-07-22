const { createApp } = Vue;


const app = createApp({

    data() {

        return {

            makes: [],

            selectedMake: "",

            selectedType: "",

            year: "",

            years: [],


            vehicleTypes: [],

            models: [],


            loading: false,

            hasSearched: false,

            toastMessage: "",


            currentPage: 1,

            pageSize: 10,

            totalPages: 0

        };

    },


    async mounted() {


        await this.loadMakes();


        const currentYear = new Date().getFullYear();


        let yearsList = [];

        for (let year = currentYear; year >= 1900; year--) {

            yearsList.push(year.toString());

        }


        this.years = yearsList;


    },


    methods: {


        async loadMakes() {

            try {

                const response = await fetch(
                    "/CarExplorer/GetMakes"
                );


                this.makes = await response.json();

            }
            catch (error) {

                console.log(error);

                this.showToast(
                    "Failed to load car makes"
                );

            }

        },



        async loadVehicleTypes() {


            this.selectedType = "";


            if (!this.selectedMake) {

                this.vehicleTypes = [];

                return;

            }



            try {


                const response = await fetch(
                    `/CarExplorer/GetVehicleTypes?makeId=${this.selectedMake}`
                );


                this.vehicleTypes =
                    await response.json();


            }
            catch (error) {


                console.log(error);


            }


        },



        async loadModels() {


            if (!this.selectedMake || !this.year) {


                this.showToast(
                    "Please select make and year"
                );


                return;

            }



            this.hasSearched = true;

            this.currentPage = 1;


            await this.getModels();


        },



        async getModels() {


            this.loading = true;


            try {


                const response = await fetch(

                    `/CarExplorer/GetModels?makeId=${this.selectedMake}&year=${this.year}&page=${this.currentPage}&pageSize=${this.pageSize}`

                );



                const data = await response.json();



                this.models = data.items;


                this.totalPages = data.totalPages;



            }

            catch (error) {


                console.log(error);


            }


            finally {


                this.loading = false;


            }


        },



        async changePage(page) {


            if (page < 1 || page > this.totalPages)

                return;



            this.currentPage = page;



            await this.getModels();


        },



        showToast(message) {


            this.toastMessage = message;


            const toastElement =
                document.getElementById("errorToast");


            const toast =
                new bootstrap.Toast(toastElement);



            toast.show();


        }


    }


});





/*
    Tom Select Component
*/


app.component("tom-select", {


    props: {


        modelValue: [String, Number],


        options: {

            type: Array,

            default: () => []

        },


        valueField: String,


        labelField: String,


        placeholder: String


    },



    emits: [

        "update:modelValue",

        "change"

    ],



    template: `

        <select ref="select"></select>

    `,



    mounted() {

        this.control = new TomSelect(

            this.$refs.select,

            {

                options: this.formatOptions(),

                maxOptions: 50,

                valueField: "value",

                labelField: "text",

                searchField: "text",

                placeholder: this.placeholder,


                onChange: (value) => {

                    this.$emit(
                        "update:modelValue",
                        value
                    );


                    this.$emit(
                        "change",
                        value
                    );

                }

            }

        );

    },



    methods: {


        formatOptions() {


            return this.options.map(item => {


                if (this.valueField) {

                    return {

                        value: item[this.valueField].toString(),

                        text: item[this.labelField]

                    };

                }


                return {

                    value: item.toString(),

                    text: item.toString()

                };


            });


        }


    },



    watch: {

        options: {

            deep: true,

            handler(newOptions) {


                if (!this.control)
                    return;



                this.control.clear();


                this.control.clearOptions();



                this.control.addOptions(
                    this.formatOptions()
                );


                this.control.refreshOptions(false);


            }

        },


        modelValue(value) {


            if (this.control && value) {

                this.control.setValue(
                    value,
                    true
                );

            }

        }

    },



    beforeUnmount() {


        if (this.control) {


            this.control.destroy();


        }


    }


});





app.mount("#carApp");