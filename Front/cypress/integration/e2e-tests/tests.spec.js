describe('Cypress', () => {
    it('is working', () => {
      cy.visit("localhost:4000")
    })

    it('can search', () => {
        cy.get('input').click().type("1")
        cy.get('#button').click()
    })
})