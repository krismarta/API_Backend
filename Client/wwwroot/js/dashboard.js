$(document).ready(function () {
    $.ajax({
        url: "https://localhost:44325/api/employees/countGender",
        type: "GET",
    }).done((result) => {
        console.log(result);
        var options = {
            chart: {
                type: "donut",
                toolbar: {
                    show: true,
                    offsetX: 0,
                    offsetY: 0,
                    tools: {
                        download: true,
                        selection: true,
                        zoom: true,
                        zoomin: true,
                        zoomout: true,
                        pan: true,
                        reset: true | '<img src="/static/icons/reset.png" width="20">',
                        customIcons: []
                    },
                    export: {
                        csv: {
                            filename: "Grafik-Gender",
                            columnDelimiter: ',',
                            headerCategory: 'Gender',
                            headerValue: 'Value',
                            dateFormatter(timestamp) {
                                return new Date(timestamp).toDateString()
                            }
                        },
                        svg: {
                            filename: "Grafik-Gender",
                        },
                        png: {
                            filename: "Grafik-Gender",
                        }
                    },
                    autoSelected: 'zoom'
                },
            },
            plotOptions: {
                pie: {
                    donut: {
                        labels: {
                            show: true,
                            name: {
                                show: true,
                            },
                            value: {
                                show: true,
                            },
                            total: {
                                show: true,
                                label: 'Total Employee',
                                formatter: function (w) {
                                    return w.globals.seriesTotals.reduce((a, b) => {
                                        return a + b
                                    }, 0)
                                }
                            }
                        }
                    }
                }

            },

            series: result.result.series,
            labels: result.result.label,
            colors: ['#F5C23E', '#E02D1B']

        };
        var chart = new ApexCharts(document.querySelector("#gender_chart"), options);
        chart.render();

    }).fail((error) => {
        Swal.fire(
            'Opps!',
            'Sepertinya terjadi kesalahan Pada Chart Gender',
            'error'
        )
    })

    //university grafik
    $.ajax({
        url: "https://localhost:44325/api/universities/getcount",
        type: "GET",
    }).done((result) => {
        console.log(result);
        var options = {
            chart: {
                type: "pie",
                toolbar: {
                    show: true,
                    offsetX: 0,
                    offsetY: 0,
                    tools: {
                        download: true,
                        selection: true,
                        zoom: true,
                        zoomin: true,
                        zoomout: true,
                        pan: true,
                        reset: true | '<img src="/static/icons/reset.png" width="20">',
                        customIcons: []
                    },
                    export: {
                        csv: {
                            filename: "Grafik-universitas",
                            columnDelimiter: ',',
                            headerCategory: 'Universitas',
                            headerValue: 'Value',
                            dateFormatter(timestamp) {
                                return new Date(timestamp).toDateString()
                            }
                        },
                        svg: {
                            filename: "Grafik-universitas",
                        },
                        png: {
                            filename: "Grafik-universitas",
                        }
                    },
                    autoSelected: 'zoom'
                },
            },
            plotOptions: {
                pie: {
                    donut: {
                        labels: {
                            show: true,
                            name: {
                                show: true,
                            },
                            value: {
                                show: true,
                            },
                        }
                    }
                }
            },
            series: result.result.series,
            labels: result.result.label,
            colors: ['#439FAF', '#3159D9']
        };

        var charta = new ApexCharts(document.querySelector("#university_chart"), options);
        charta.render();
    }).fail((error) => {
        Swal.fire(
            'Opps!',
            'Sepertinya terjadi kesalahan Pada Chart University',
            'error'
        )
    })

});

