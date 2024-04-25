window.addEventListener('DOMContentLoaded', event => {
    const datatablesSimple = document.getElementById('datatablesSimple');
    if (datatablesSimple) {
        new simpleDatatables.DataTable(datatablesSimple, {
            "decimal": "",
            "emptyTable": "Brak dostępnych danych",
            "info": "Wyświetlanie od _START_ do _END_ z _TOTAL_ pozycji",
            "infoEmpty": "Wyświetlanie 0 do 0 z 0 pozycji",
            "infoFiltered": "(przefiltrowane z _MAX_ wszystkich pozycji)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Pokaż _MENU_ pozycji",
            "loadingRecords": "Ładowanie...",
            "processing": "",
            "search": "Szukaj:",
            "zeroRecords": "Nie znaleziono pasujących rekordów",
            "paginate": {
                "first": "Pierwsza",
                "last": "Ostatnia",
                "next": "Następna",
                "previous": "Poprzednia"
            },
            "aria": {
                "sortAscending": ": aktywuj, aby posortować kolumnę rosnąco",
                "sortDescending": ": aktywuj, aby posortować kolumnę malejąco"
            }
        });
    }
});
