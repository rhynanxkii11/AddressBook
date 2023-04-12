function myFunction() {

    let input, filterData, table, tr, td, i, txtValue;
    input = document.getElementById("myInput");
    filterData = input.value.toUpperCase();
    table = document.getElementById("mySearchTable");
    tr = table.getElementsByTagName("tr");

    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[1];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filterData) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}